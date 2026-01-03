# AI Nedir, Ne Değildir, Kodla İlişkisi

## AI Kavramına Giriş

Yapay zeka (Artificial Intelligence, AI) **makinelerin insan benzeri görevleri yerine getirme yeteneğidir**.
AI sadece "robotlar" veya "bilinçli makineler" demek değildir. AI, **veri, algoritma ve karar mekanizması** ile işler.

## AI Ne Değildir

- AI **sihirli bir çözüm değildir**: hazır kütüphaneyi çalıştırmak her zaman doğru sonucu vermez.
- AI, **sadece veri girişiyle kendiliğinden öğrenmez**; model ve algoritma tasarımı gerekir.
- AI, **insan zekasının birebir taklidi değildir**; belirli görevlerde optimizasyon sağlar.

## AI Türleri (Kısa ve Öz)

- **Dar AI (Narrow AI):** Tek bir görev için optimize edilmiş, örn. satranç oynayan program.
- **Genel AI (General AI):** İnsan gibi çok görevli öğrenme yeteneği (henüz yok).
- **Makine Öğrenmesi (Machine Learning, ML):** AI'nin alt dalı, veri ile model öğrenir.
- **Derin Öğrenme (Deep Learning, DL):** Çok katmanlı neural network ile öğrenme, özellikle görüntü, ses ve dil için kullanılır.

## Kodla İlişkisi

AI projeleri yazılım geliştirme ile iç içedir:

- **Veri işleme:** Python, C#, SQL, CSV, JSON
- **Model oluşturma:** Neural network, reinforcement learning, decision trees
- **Algoritma implementasyonu:** Ağırlık güncellemeleri, geri yayılım, optimizasyon

> AI bir "hazır kütüphane çalıştırma" işi değildir; kod yazarak model tasarlamak ve davranışlarını test etmek gerekir.

## Ping-Pong ile Örnek Bağlantı

- Bu projede AI, **masa tenisi raketi kontrol etmek** için öğrenir.
- Kod tarafı: top pozisyonunu oku, raketi hareket ettir, ödül-ceza mekanizmasını uygula.
- Amaç: AI'nin **adım adım nasıl öğrendiğini görmek**, "siyah kutu" düşüncesini kırmak.

Sonraki dokümanda, AI için
[**öğrenme kavramı**](./01-WhatIsLearning.md) konusuna geçeceğiz.

## Ayrıca Bakınız

- [Ana Sayfa](../../README.md)
- *AI nedir, ne değildir, kodla ilişkisi*
- &gt; [Öğrenme kavramı, supervised / unsupervised / reinforcement](./01-WhatIsLearning.md)
- [Yapay nöron, girdi/ağırlık/bias, basit örnek](./02-Neuron.md)
- [Mini neural network, hidden layer, ileri beslemeli ağ](./03-NeuralNetwork.md)
- [Ödül ve ceza, self-play, temel RL mantığı](./04-ReinforcementLearning.md)
- [Hassabis yaklaşımı, self-play, modüler mimari](./05-WhyThisArchitecture.md)
- [PingPongAI.App Gerekçeleri](./06-PingPongAI.App.md)
- [PingPongAI.App Oyun Kuralları](./07-PingPongAI.App.Rules.md)
- [Kural Tabanlı Ajan Yaklaşımı](./08-RuleBased.md)
- [AIAgent - Supervised Control Yaklaşımı](./09-AIAgent-SupervisedControl.md)
