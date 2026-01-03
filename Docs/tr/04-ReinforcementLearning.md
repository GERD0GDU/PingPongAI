# Reinforcement Learning (Pekiştirmeli Öğrenme)

Bu doküman, bir neural network'ün **nasıl öğrendiğini** anlatır.

Buradaki öğrenme:

- öğretmenli değildir
- doğru cevap verilmez
- "şunu yap" denmez

Bunun yerine:

- deneme vardır
- sonuç vardır
- geri bildirim vardır

## Öğrenme Problemi Nerede Başlar?

Bir neural network:

- girdi alır
- çıktı üretir

Ama şunu bilmez:

- bu çıktı iyi mi?
- kötü mü?
- işe yaradı mı?

Öğrenmenin başlayabilmesi için ağın şu soruya cevap alması gerekir:

> Yaptığım şey doğru muydu?

Bu cevap **ödül - ceza** mekanizmasıyla verilir.

## Reinforcement Learning Nedir?

Reinforcement Learning (RL):

- bir ajanın
- bir ortam içinde
- eylemler yaparak
- sonuçlardan ders çıkarmasıdır

Burada anahtar kavramlar şunlardır:

- ajan (agent)
- ortam (environment)
- eylem (action)
- ödül (reward)

## Ajan (Agent)

Ajan:

- karar veren taraftır
- bizim projemizde neural network'tür

Ajan:

- dünyayı gözlemler
- bir eylem seçer
- sonucu bekler

Ajan:

- ne yapması gerektiğini başta bilmez
- zamanla öğrenir

## Ortam (Environment)

Ortam:

- ajanın içinde bulunduğu dünyadır

Ping-pong için ortam:

- oyun sahası
- top
- raket
- fizik kuralları

Ortam:

- ajana durum bilgisi verir
- ajanın eylemine tepki verir

## Eylem (Action)

Eylem:

- ajanın seçtiği harekettir

Ping-pong için örnek eylemler:

- raketi yukarı hareket ettir
- raketi aşağı hareket ettir
- hiçbir şey yapma

Ajan:

- her adımda bir eylem seçer
- seçimin sonucuna katlanır

## Ödül - Ceza (Reward)

Öğrenmenin kalbi burasıdır.

Ödül:

- sayısal bir geri bildirimdir
- "iyi yaptın" demenin matematiksel halidir

Örnek:

- topu karşıladın -> +1
- top kaçtı -> -1
- gereksiz hareket -> küçük negatif ödül

Önemli nokta:

- ödül ahlaki değildir
- anlam taşımaz
- sadece sayıdır

## Öğrenme Nasıl Gerçekleşir?

Süreç döngüseldir:

1. ajan durumu gözlemler
2. bir eylem seçer
3. ortam tepki verir
4. ödül hesaplanır
5. ağ kendini biraz değiştirir

Bu döngü:

- binlerce
- milyonlarca
- bazen milyarlarca kez

tekrar eder.

Öğrenme:

- tek bir adımda olmaz
- sabır ister

## Self-Play Kavramı

Self-play:

- ajanın kendisiyle oynamasıdır

Burada:

- öğretmen yoktur
- örnek yoktur
- kopyalama yoktur

Ajan:

- kendi hatalarından öğrenir
- kendi başarılarını pekiştirir

Bu yaklaşım:

- [AlphaGo](https://en.wikipedia.org/wiki/AlphaGo)
- [AlphaZero](https://en.wikipedia.org/wiki/AlphaZero)

gibi sistemlerin temelidir.

## Bilinç - Sezgi - Sihir

Bu noktada bir karışıklık olur.

Ajan:

- 🔥 bilinçli değildir
- ne yaptığını "anlamaz"
- kazanmak istemez

Ama:

- ⚡ sezgisel davranışlar sergiler

Bu:

- ✨ sihir değildir
- istatistiktir
- tekrarın sonucudur

## PingPongAI Bağlamı

Bu projede reinforcement learning:

- kuralları öğretmez
- doğru hamleyi söylemez
- "şuraya vur" demez

Sadece şunu yapar:

> Yaptığın şeyin sonucu buydu.

Zamanla:

- iyi sonuç veren davranışlar artar
- kötü sonuç verenler azalır

Bu kadar.

> Amaç: Sistemi, beklenen toplam ödülü maksimize eden davranışlara doğru istatistiksel olarak normalize etmektir.

Artık elimizde:

- neural network var
- reinforcement learning var

Şimdi soru şu:

> Bunları neden bu şekilde bir mimaride birleştiriyoruz?

Bir sonraki dokümanda, [**Demis Hassabis'in yaklaşımı**](./05-WhyThisArchitecture.md) ele alınacak.

## Ayrıca Bakınız

- [Ana Sayfa](../../README.md)
- [AI nedir, ne değildir, kodla ilişkisi](00-WhatIsAI.md)
- [Öğrenme kavramı, supervised / unsupervised / reinforcement](./01-WhatIsLearning.md)
- [Yapay nöron, girdi/ağırlık/bias, basit örnek](./02-Neuron.md)
- [Mini neural network, hidden layer, ileri beslemeli ağ](./03-NeuralNetwork.md)
- *Ödül ve ceza, self-play, temel RL mantığı*
- &gt; [Hassabis yaklaşımı, self-play, modüler mimari](./05-WhyThisArchitecture.md)
- [PingPongAI.App Gerekçeleri](./06-PingPongAI.App.md)
- [PingPongAI.App Oyun Kuralları](./07-PingPongAI.App.Rules.md)
- [Kural Tabanlı Ajan Yaklaşımı](./08-RuleBased.md)
- [AIAgent - Supervised Control Yaklaşımı](./09-AIAgent-SupervisedControl.md)
