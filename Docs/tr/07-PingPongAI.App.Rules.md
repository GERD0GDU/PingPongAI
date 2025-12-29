# PingPongAI.App Oyun Kuralları

Bu doküman, PingPongAI.App masaüstü uygulamasında kullanılan temel oyun kurallarını tanımlar.

Tanımlanan kurallar; oyunun deterministik çalışmasını, AI eğitimi sırasında tutarlı davranışlar üretilmesini ve gözlemlenebilir bir simülasyon ortamı oluşturulmasını amaçlar.

## Oyunun Başlatılması

- Uygulama ilk açıldığında oyun otomatik olarak başlar
- Oyuna başlayacak oyuncu rastgele seçilir
- Rastgele seçim, her yeni başlangıçta tarafsızlığı korumak için yapılır

## Topun Oyuna Girişi

- Oyuna başlayan oyuncunun raketinin herhangi bir rastgele noktasından top oyuna çıkar
- Top, seçilen oyuncunun tarafından karşı oyuncu tarafına doğru gönderilir
- Topun ilk yönü ve hızı, oyunun başlangıç parametrelerine göre belirlenir

## Çarpışma ve Sekme Davranışı

- Karşı oyuncu topu raketiyle karşılarsa, çarpışma noktası hesaplanır
- Topun rakete değdiği noktaya göre:

- sekme açısı değişir
- topun hızı farklı şekilde ayarlanabilir

- Raketin orta noktasına yakın vuruşlar daha dengeli sekmeler üretir
- Raketin uç noktalarına yakın vuruşlar daha keskin açılar oluşturur

Bu mekanizma, oyunda çeşitlilik ve öğrenilebilir davranış alanı oluşturmak için bilinçli olarak eklenmiştir.

## Puanlama Kuralları

- Oyunculardan biri topu karşıladığında oyun devam eder
- Oyunculardan biri topu kaçırırsa:

- karşı oyuncuya 1 puan verilir

- Puanlama yalnızca topun kaçırılması durumunda gerçekleşir
- Oyun sırasında başka hiçbir durum puan üretmez

## Tur Sonu ve Yeniden Başlatma

- Oyunculardan biri topu kaçırdığında tur sona erer
- Top, başlangıç konumuna geri alınır
- Yeni tur:

- bir önceki turu kazanan oyuncu tarafından başlatılır
- top, kazanan oyuncunun raketinin herhangi bir rastgele noktasından çıkar
- top karşı oyuncu tarafına doğru gönderilir

Bu yaklaşım, kazanan oyuncunun avantajlı ama deterministik olmayan bir başlangıç yapmasını sağlar.

## Genel İlkeler

- Oyun kuralları basit ve nettir
- Fizik ve çarpışma hesapları deterministiktir
- Aynı başlangıç koşullarında aynı sonuçlar üretilir
- Kurallar, AI veya insan oyuncu ayrımı yapmaz
- Oyun motoru açısından tüm oyuncular eşit kabul edilir

Bu kurallar, `PingPongAI.App` uygulamasının hem oynanabilir bir oyun hem de güvenilir bir AI eğitim ortamı olmasını sağlar.

## v1.0-basic Kapsam Özeti

Bu sürüm, `PingPongAI.App` uygulamasının oynanabilir ve deterministik bir temel oyun çekirdeği oluşturmasını hedefler.

Bu kapsamda:

- Fizik kuralları ve çarpışma hesapları netleştirilmiştir
- İnsan - insan oyuncu etkileşimi desteklenmiştir
- AI entegrasyonu için gerekli altyapı hazırlanmıştır

Bu sürüm, ileride eklenecek AI ve reinforcement learning bileşenleri için referans noktası olarak kullanılacaktır.

## Tamamlanan Özellikler (v1.0-basic)

- [x] Temel Ping Pong oyun mekaniği
- [x] Deterministik top hareketi ve çarpışma hesapları
- [x] Raket - top çarpışma noktasına bağlı sekme davranışı
- [x] İki oyunculu (insan - insan) oynanış
- [x] Klavye kontrollü raket hareketleri
- [x] Puanlama sistemi
- [x] Tunneling probleminin RayVsRect yaklaşımı ile çözülmesi
- [x] Oyun turlarının deterministik şekilde yeniden başlatılması

## Bilinçli Olarak Kapsam Dışı Bırakılanlar

Aşağıdaki konular bu sürümde bilinçli olarak uygulanmamıştır:

- [ ] AI veya bot oyuncu davranışları
- [ ] Reinforcement learning altyapısı
- [ ] Oyun içi ödül - ceza mekanizmaları
- [ ] Model eğitimi veya inference süreçleri
- [ ] Görsel efektler veya animasyon iyileştirmeleri
- [ ] Ses efektleri
- [ ] Network veya çok oyunculu (online) destek

Bu konular, ilerleyen sürümlerde kontrollü ve aşamalı şekilde ele alınacaktır.

Dokümanın bu noktasına kadar yapılan değişiklikler ve güncellemelere ait kodlara [v1.0-basic](https://github.com/GERD0GDU/PingPongAI/tree/v1.0-basic) etiketinden erişebilirsiniz.

Bir sonraki konuda [Kural Tabanlı Ajan Yaklaşımı](./08-RuleBased.md) yapısı anlatılacaktır.

## Ayrıca Bakınız

- [Ana Sayfa](../../README.md)
- [AI nedir, ne değildir, kodla ilişkisi](00-WhatIsAI.md)
- [Öğrenme kavramı, supervised / unsupervised / reinforcement](./01-WhatIsLearning.md)
- [Yapay nöron, girdi/ağırlık/bias, basit örnek](./02-Neuron.md)
- [Mini neural network, hidden layer, ileri beslemeli ağ](./03-NeuralNetwork.md)
- [Ödül ve ceza, self-play, temel RL mantığı](./04-ReinforcementLearning.md)
- [Hassabis yaklaşımı, self-play, modüler mimari](./05-WhyThisArchitecture.md)
- [PingPongAI.App Gerekçeleri](./06-PingPongAI.App.md)
- *PingPongAI.App Oyun Kuralları*
- &gt; [Kural Tabanlı Ajan Yaklaşımı](./08-RuleBased.md)
