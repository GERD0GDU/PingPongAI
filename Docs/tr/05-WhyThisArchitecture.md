# Neden Bu Mimari?

Bu projede kullanılan mimari tesadüfi değildir.

Ama aynı zamanda:

- akademik bir referans mimari değildir
- endüstriyel ölçekte optimize edilmemiştir
- "en doğru yol" iddiası taşımaz

Bu mimari, **öğrenmeyi görünür kılmak** için seçilmiştir.

## İlham: Demis Hassabis Yaklaşımı

Bu projenin düşünsel arka planında, DeepMind kurucularından Demis Hassabis'in şu fikri vardır:

> Karmaşık problemleri çözmek için\
karmaşık kurallar yazmak yerine\
öğrenebilen sistemler kurmak

Bu yaklaşımda:

- sistem ne yapacağını bilmez
- kurallar minimumda tutulur
- öğrenme süreci merkeze alınır

Ping-pong oyunu bu yüzden seçilmiştir.

## Neden Self-Play?

Self-play şu anlama gelir:

> Sistemin kendi kendine oynaması - öğrenmesi

Bu sayede:

- dışarıdan "iyi davranış" tanımı verilmez
- insan stratejileri kopyalanmaz
- tek referans başarı - başarısızlıktır

AI, başka bir AI ile oynarken:

- hatalarını tekrar tekrar görür
- kendi sınırlarını zorlar
- beklenmedik ama işe yarayan davranışlar geliştirir

Bu, öğrenmenin en saf halidir.

## Neden Modüler Yapı?

Projede şu parçalar bilinçli olarak ayrılmıştır:

- oyun motoru
- fizik hesapları
- AI ajanı
- öğrenme mekanizması
- görselleştirme katmanı

Sebebi şudur:

> Öğrenme kodu ile oyun kodu birbirine karışırsa\
neyin neden olduğunu anlayamazsın

Modüler yapı sayesinde:

- AI olmadan oyun çalışır
- oyun olmadan AI test edilebilir
- her parça tek başına anlaşılabilir

Bu proje; bir "ürün" değil bir **inceleme laboratuvarıdır**.

## Neden Hazır Kütüphane Yok?

Bu projede:

- TensorFlow yok
- PyTorch yok
- hazır RL framework yok

Çünkü amaç:

- sonucu almak değil
- süreci anlamak

Hazır kütüphaneler:

- çok güçlüdür
- ama öğrenme sürecini gizler

Burada ise her ağırlık değişimi bilinçli olarak görünür kılınır.

## Mimari Ne Sağlar?

Bu mimari sayesinde:

- AI'nin nasıl karar verdiği izlenebilir
- hataların kaynağı anlaşılabilir
- "burada neden böyle davrandı?" sorusu sorulabilir

Bu soruların sorulabildiği bir sistem:

> öğrenmeye gerçekten açıktır

## Bu Mimari Ne Yapmaz?

Bu mimari:

- 🔥 insan gibi düşünmez
- 🧠 sezgi üretmez
- ✨ sihir yapmaz

Yaptığı tek şey şudur:

> Deneyim - geri bildirim - ayarlama

Ama bu üçlü bir araya geldiğinde çok güçlü bir yapı ortaya çıkar.

## Sonuç

Bu mimari:

- basit görünür
- yavaş öğrenir
- mütevazıdır

Ama tam da bu yüzden:

- öğreticidir
- şeffaftır
- güvenilirdir

PingPongAI'nin mimarisi, AI'yi yüceltmek için değil **anlaşılır kılmak için** vardır.

## Ayrıca Bakınız

- [Ana Sayfa](../../README.md)
- [AI nedir, ne değildir, kodla ilişkisi](00-WhatIsAI.md)
- [Öğrenme kavramı, supervised / unsupervised / reinforcement](./01-WhatIsLearning.md)
- [Yapay nöron, girdi/ağırlık/bias, basit örnek](./02-Neuron.md)
- [Mini neural network, hidden layer, ileri beslemeli ağ](./03-NeuralNetwork.md)
- [Ödül ve ceza, self-play, temel RL mantığı](./04-ReinforcementLearning.md)
- *Hassabis yaklaşımı, self-play, modüler mimari*
