# AIAgent - Supervised Control Yaklaşımı

Bu doküman, `PingPongAI` projesinde kullanılan ilk yapay zeka ajanı olan **`AIAgent`** sınıfının öğrenme yaklaşımını ve bu yaklaşımın bilinçli sınırlarını açıklamak amacıyla hazırlanmıştır.

Bu aşamada uygulanan yöntem, klasik anlamda pekiştirmeli öğrenme değildir. Bunun yerine, **beklenen davranışın dışarıdan tanımlandığı**, kontrollü ve gözlemlenebilir bir öğrenme modeli tercih edilmiştir.

Amaç, AI davranışının temellerini anlaşılır hale getirmek ve daha karmaşık öğrenme yöntemleri için sağlam bir zemin oluşturmaktır.

## AIAgent Nedir?

`AIAgent`, `IPongAgent` arabiriminden türetilmiş bir ajan sınıfıdır.

Bu ajan:

- Oyun durumunu (state) gözlemler
- Paddle için yukarı - aşağı - dur kararlarından birini üretir
- Bu kararın **doğru olup olmadığını kendisi değerlendirmez**

Doğru davranış, sistem dışından hesaplanır ve ajana bildirilir.

Bu nedenle AIAgent:

- Kendi başına öğrenen bir yapı değildir
- Davranışlarını, kendisine söylenen beklentiye göre optimize eder
- Pekiştirmeli öğrenmeden farkı, kendi çıktısına göre ödül veya ceza sinyali almak yerine, dışarıdan hesaplanan beklenen çıktıyı üretmeye çalışmasıdır

## Beklenen Davranışın Hesaplanması

Beklenen paddle hareketi, `PingPongAI.Core.Simulation.TargetCalculator` sınıfı tarafından hesaplanır.

Bu hesaplama sırasında:

- Topun paddle'a göre yatay yönü (yaklaşıyor mu - uzaklaşıyor mu)
- Topun paddle'a göre normalize edilmiş dikey (Y) konumu

gibi oyun durumuna ait bilgiler kullanılır.

`TargetCalculator` çıktısı şunu temsil eder:

> Bu oyun durumunda paddle için doğru davranış ne olmalıydı?

Bu değer, AIAgent için bir **öğretmen sinyali** görevi görür.

```csharp
using PingPongAI.Core.Math;
using PingPongAI.Core.States;

namespace PingPongAI.Core.Simulation
{
    public static class TargetCalculator
    {
        public static ResultPair Calculate(
            GameState previous,
            GameState current)
        {
            double leftReward = ComputeExpectedForPaddle(current, current.LeftPaddle);
            double rightReward = ComputeExpectedForPaddle(current, current.RightPaddle);

            return new ResultPair(leftReward, rightReward);
        }

        private static double ComputeExpectedForPaddle(GameState state, PaddleState paddle)
        {
            if (paddle.Side == PaddleSide.Left && state.Ball.Velocity.X > 0)
                return 0.0;
            else if (paddle.Side == PaddleSide.Right && state.Ball.Velocity.X < 0)
                return 0.0;

            double expected = 0.0;
            double relativeY = (state.Ball.CenterY - paddle.CenterY) / (paddle.Height / 2);
            relativeY = MathEx.Clamp(relativeY, -1.0, 1.0);

            expected += relativeY;

            return expected;
        }
    }
}
```

## Öğrenme Süreci (Train Mekanizması)

AIAgent, `Train(expected)` yöntemi ile eğitilir.

Bu süreçte:

- Ajan kendi kararını üretir
- Dışarıdan hesaplanan `expected` değeri ajana iletilir
- Üretilen çıktı ile beklenen çıktı arasındaki fark hesaplanır
- Ajan, iç ağırlıklarını bu farka göre günceller

Bu yaklaşım sayesinde:

- Öğrenme süreci deterministiktir
- Hangi davranışın neden değiştiği izlenebilir
- Hatalar kolayca analiz edilebilir

Bu yapı, öğretmenli öğrenmeye benzer bir kontrol modeli sunar.

## Bu Yaklaşım Ne Değildir?

Bu aşamada uygulanan yöntem:

- Pekiştirmeli öğrenme değildir
- Ödül - ceza mekanizması içermez
- Ajan kendi eylemlerinin sonucunu değerlendirmez
- Uzun vadeli strateji geliştirmez

AIAgent:

- Kazanmak istemez
- Sonucu umursamaz
- Sadece kendisine söylenen davranışı öğrenmeye çalışır

Bu sınırlamalar bilinçli olarak tercih edilmiştir.

## Mimari Neden Ayrı Tutuldu?

Bu öğrenme yaklaşımı, ileride eklenecek pekiştirmeli öğrenme ajanı ile **bilinçli olarak ayrıştırılmıştır**.

Planlanan yapı:

- Mevcut AIAgent - supervised control yaklaşımı
- Yeni bir AIAgent türevi - reinforcement learning yaklaşımı

Yeni ajan:

- Aynı `IPongAgent` arabirimini kullanabilir
- Ancak farklı bir `AgentType` ile tanımlanabilir
- Kendi eylemlerine göre ödül - ceza alarak öğrenir

Bu konu, bir sonraki dokümanın kapsamındadır.

## Uygulama Tarafındaki Entegrasyon

PingPongAI.App WPF uygulaması şu anda üç farklı oyuncu tipini desteklemektedir:

- HumanAgent
- RuleBasedAgent
- AIAgent

Bu ajanlar:

- Sol veya sağ paddle fark etmeksizin
- Birbirleriyle serbestçe eşleştirilebilir
- Oyun motoru açısından eşit kabul edilir

Arayüzde bulunan **Enable AI Training** checkbox'ı:

- Her iki taraftaki `AIAgent` için
- Öğrenme sürecini aktif veya pasif hale getirir

İlerleyen adımlarda:

- Sol ve sağ AI için
- Ayrı eğitim kontrol checkbox'ları eklenmesi planlanmaktadır

## Kod Versiyonlama ve Referans

Bu dokümanda anlatılan `AIAgent` öğrenme modeli, belirli bir kod durumuna karşılık gelmektedir.

Bu nedenle:

- Mevcut kodlar GitHub üzerinde bir tag ile sabitlenecektir
- Doküman ve kod bire bir eşleşecektir
- İleride yapılan değişiklikler bu aşamayı bozmayacaktır

Bu yaklaşım, öğrenme sürecinin hem teorik hem de pratik olarak izlenebilmesini sağlar.

## Sonuç

Bu aşamada geliştirilen `AIAgent`:

- Öğrenmenin temel mekaniklerini anlamayı sağlar
- AI davranışlarını şeffaf ve kontrol edilebilir hale getirir
- Pekiştirmeli öğrenmeye geçiş için sağlam bir referans noktası oluşturur

Bu yapı, yapay zekayı gizemli hale getirmek için değil, **anlaşılır kılmak için** tasarlanmıştır.

Dokümanın bu noktasına kadar yapılan değişiklikler ve güncellemelere ait kodlara [v1.2-supervised-control](https://github.com/GERD0GDU/PingPongAI/tree/v1.2-supervised-control) etiketinden erişebilirsiniz.

Bir sonraki dokümanda, kendi çıktılarının sonucuna göre öğrenen ve ödül - ceza mekanizması kullanan ajan mimarisi ele alınacaktır.

## Ayrıca Bakınız

- [Ana Sayfa](../../README.md)
- [AI nedir, ne değildir, kodla ilişkisi](00-WhatIsAI.md)
- [Öğrenme kavramı, supervised / unsupervised / reinforcement](./01-WhatIsLearning.md)
- [Yapay nöron, girdi/ağırlık/bias, basit örnek](./02-Neuron.md)
- [Mini neural network, hidden layer, ileri beslemeli ağ](./03-NeuralNetwork.md)
- [Ödül ve ceza, self-play, temel RL mantığı](./04-ReinforcementLearning.md)
- [Hassabis yaklaşımı, self-play, modüler mimari](./05-WhyThisArchitecture.md)
- [PingPongAI.App Gerekçeleri](./06-PingPongAI.App.md)
- [PingPongAI.App Oyun Kuralları](./07-PingPongAI.App.Rules.md)
- [Kural Tabanlı Ajan Yaklaşımı](./08-RuleBased.md)
- *AIAgent - Supervised Control Yaklaşımı*
