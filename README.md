# PingPongAI

[![license](https://img.shields.io/github/license/GERD0GDU/PingPongAI.svg)](https://github.com/GERD0GDU/PingPongAI/blob/main/LICENSE)

[![watchers](https://img.shields.io/github/watchers/GERD0GDU/PingPongAI.svg?style=social&label=Watch)](https://GitHub.com/GERD0GDU/PingPongAI/watchers/)
[![forks](https://img.shields.io/github/forks/GERD0GDU/PingPongAI.svg?style=social&label=Fork)](https://GitHub.com/GERD0GDU/PingPongAI/network/)
[![stars](https://img.shields.io/github/stars/GERD0GDU/PingPongAI.svg?style=social&label=Star)](https://GitHub.com/GERD0GDU/PingPongAI/stargazers/)

Yapay zekâ kontrollü masa tenisi oyunu - C#/.NET Core kullanarak sıfırdan kendi sinir ağınızı eğitin.

## 🌐 Çok Dilli Destek

[English](./Docs/en/ReadMe.md)

> *Diğer dillere çeviri işlemi AI tarafından yapılmıştır.*

## Neden Bu Proje Var?

Bu proje bir ürün geliştirmek için değil, bir kavramı anlamak için oluşturuldu.

AI kavramı bugün çok sık kullanılıyor. Ancak çoğu zaman şu şekilde sunuluyor:

- hazır kütüphaneler
- siyah kutu modeller
- karmaşık matematik
- "bunu kullan, sonucu al" yaklaşımı

Bu yaklaşım, özellikle yazılım geliştirme kökenli ama AI geçmişi olmayan geliştiriciler için şu hissi yaratıyor:

> AI bana uzak bir şey. Sanki sadece akademisyenlerin ya da büyük şirketlerin alanı!

Bu proje tam olarak bu algıyı kırmak için var.

## İlham Kaynağı

Bu projeyi düşünmeme sebep olan şey, yotube'da DeepMind hakkında hazırlanmış aşağıdaki belgeseli izlemem oldu:

[![The Thinking Game](https://img.youtube.com/vi/d95J8yzvjbQ/maxresdefault.jpg)](https://youtu.be/d95J8yzvjbQ)

Belgeselde Demis Hassabis'in şu yaklaşımı özellikle dikkatimi çekti:

- AI'yi kapalı bir teknoloji olarak değil
- insanlığın ortak problemi olarak ele alması
- üretilen bilgiyi mümkün olduğunca açık paylaşma isteği

"Dünya için faydalı bir şeyler yapmak" motivasyonu, AI gibi soyut ve karmaşık bir alanın aslında ne kadar insani bir amaç taşıyabileceğini gösteriyordu.

Bu proje, o yaklaşımın küçük - mütevazı bir yansımasıdır.

## Neden Ping-Pong?

Belgeselde gördüğüm ping-pong oyunu, bu proje için bilinçli bir tercihtir.

Çünkü ping-pong:

- kuralları çok basit
- gözlemlenebilir bir çevreye sahip
- karar - sonuç ilişkisi net
- başarı ve başarısızlık anında görülebilir

Bu özellikler sayesinde ping-pong, AI öğrenme mantığını anlatmak için ideal bir laboratuvar sunar.

Amaç:

- "iyi oynayan" bir AI yazmak değil
- AI'nin nasıl öğrendiğini adım adım görmek

## Bu Proje Kimler İçin?

Bu proje özellikle şu kişiler için tasarlandı:

- AI konusunda hiç deneyimi olmayan yazılım geliştiriciler
- "AI arkaplanında ne dönüyor?" diye merak edenler
- hazır kütüphaneler kullanmadan öğrenmek isteyenler
- Türkçe, sade ve açıklayıcı bir kaynak arayanlar

Ben de dahil.

## Açık Kaynak Olmasının Sebebi

Bu proje baştan sona açık kaynak olarak tasarlandı.

Çünkü:

- AI kolektif bir bilgi birikimidir
- öğrenmenin en iyi yolu paylaşmaktır
- bu repo bir vitrin değil, bir öğrenme defteridir
- Kod kadar dokümantasyonun da önemli olmasının sebebi budur.

Her klasör, her dosya, her satır şu soruya cevap vermelidir:

> Bu ne işe yarıyor - neden böyle?

## Bu Doküman Nasıl Hazırlandı?

Bu doküman, tek başına yazılmış bir manifesto değil.

Bu repo'daki dokümantasyon ve proje yapısı:

- konuyu öğrenmeye çalışan bir yazılım geliştirici
- ve bir yapay zeka modeli

arasındaki diyaloglar sonucunda şekillendi.

Ama burada önemli bir nokta var:

Yapay zeka bu dokümanı tek başına yazmadı.

İnsan tarafının:

- soruları
- itirazları
- sadeleştirme talepleri
- "bu böyle olmasın" dediği noktalar

metnin yönünü belirledi.

Bu yüzden ortaya çıkan içerik:

- kusursuz bir bilgi aktarımı iddiası taşımaz
- öğrenme sürecini olduğu gibi yansıtır
- bilinçli olarak sade ve açıklayıcı tutulmuştur

Bu şeffaflık, bu projenin temel prensiplerinden biridir.

Sonraki dokümanda, [**AI nedir**](./Docs/tr/00-WhatIsAI.md) sorusuna yanıt arayacağız.

## Ayrıca Bakınız

- [AI nedir, ne değildir, kodla ilişkisi](./Docs/tr/00-WhatIsAI.md)
- [Öğrenme kavramı, supervised / unsupervised / reinforcement](./Docs/tr/01-WhatIsLearning.md)
- [Yapay nöron, girdi/ağırlık/bias, basit örnek](./Docs/tr/02-Neuron.md)
- [Mini neural network, hidden layer, ileri beslemeli ağ](./Docs/tr/03-NeuralNetwork.md)
- [Ödül ve ceza, self-play, temel RL mantığı](./Docs/tr/04-ReinforcementLearning.md)
- [Hassabis yaklaşımı, self-play, modüler mimari](./Docs/tr/05-WhyThisArchitecture.md)
- [PingPongAI.App Gerekçeleri](./Docs/tr/06-PingPongAI.App.md)
- [PingPongAI.App Oyun Kuralları](./Docs/tr/07-PingPongAI.App.Rules.md)
