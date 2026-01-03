# Öğrenme Nedir?

Bu dokümanda "öğrenme" kavramını, yapay zeka ve yazılım geliştirme perspektifinden ele alacağız.

Amacımız akademik tanımlar ezberlemek değil.
Bir sistemin ne zaman "öğreniyor" dediğimizi gerçekten anlamak.

## Öğrenme Dediğimiz Şey Aslında Ne?

Günlük hayatta öğrenme dediğimiz şey genelde şudur:

- bir hata yaparız
- sonucunu görürüz
- bir dahaki sefere davranışımızı değiştiririz

Yapay zeka tarafında da temel fikir aynıdır.

Bir sistem:

- bir durumla karşılaşır
- bir karar verir
- bu kararın sonucunu gözlemler
- gelecekte benzer durumda farklı davranmayı dener

Eğer bu döngü varsa, ortada bir öğrenme süreci vardır.

## Öğrenme ile Kural Yazmak Arasındaki Fark

Geleneksel yazılım geliştirmede genelde şu yaklaşım vardır:

> Eğer A olursa, B yap.

Bu bir kuraldır.
Kural sayısı arttıkça sistem karmaşıklaşır ve esnekliğini kaybeder.

Öğrenen sistemlerde ise:

> A olunca ne yapmam gerektiğini zamanla öğrenirim.

Yani geliştirici:

- tüm kuralları tek tek yazmaz
- sadece öğrenme ortamını ve geri bildirimi tanımlar

Bu fark, AI ile klasik yazılım arasındaki en kritik ayrımdır.

## Yapay Zekada Öğrenme Türleri

AI literatüründe öğrenme genelde üç ana başlıkta incelenir.

### 1. Supervised Learning

- Sisteme hem girdi hem de doğru çıktı verilir
- "Bu görüntü kedi, bu köpek" gibi
- Model, doğru cevabı taklit etmeyi öğrenir

Avantajı:

- Kontrolü kolaydır
- Sonuçlar tahmin edilebilirdir

Dezavantajı:

- Etiketli veri gerektirir
- Gerçek hayatta her zaman doğru cevap yoktur

### 2. Unsupervised Learning

- Sisteme sadece veri verilir
- Doğru cevap yoktur
- Model, verideki benzerlikleri ve yapıları keşfeder

Örnekler:

- Kümeleme
- Anomali tespiti

Bu yaklaşım daha keşif odaklıdır.

### 3. Reinforcement Learning

Bu projede bizi en çok ilgilendiren öğrenme türü budur.

- Sistem bir ortam içindedir
- Aksiyon alır
- Ödül veya ceza alır
- Amacı uzun vadede toplam ödülü maksimize etmektir

Burada doğru cevap yoktur.
Sadece sonuçların geri bildirimi vardır.

## Öğrenme Neden Zaman Alır?

Öğrenme süreci genellikle:

- deneme
- hata
- tekrar

üzerine kuruludur.

Başlangıçta sistem:

- kötü kararlar verir
- anlamsız hareketler yapar
- başarısız olur

Bu beklenen ve istenen bir durumdur.

Öğrenme, ilk denemede doğru sonucu bulmak değil,
zamanla daha az hata yapmaktır.

## Ping-Pong Oyunu ile Öğrenme

Bu projede öğrenme şu soruya indirgenir:

> Raketimi nasıl hareket ettirirsem topu kaçırmam?

AI tarafı için:

- Topun konumu bir girdidir
- Raketin hareketi bir aksiyondur
- Topu karşılamak bir ödüldür
- Kaçırmak bir cezadır

Sistem, binlerce oyun oynayarak:

- hangi durumda ne yaparsa daha iyi sonuç aldığını
- zamanla öğrenir

Burada kimse AI'ye "şuraya git" demez.
Sadece sonuçları gösterir.

## Özet

- Öğrenme, davranışın sonuçlara göre değişmesidir
- AI'de öğrenme, kural yazmak yerine ortam tasarlamaktır
- Reinforcement learning, deneme-yanılma üzerine kuruludur
- Ping-pong, bu kavramı görmek için ideal bir örnektir

Sonraki dokümanda, bu öğrenmenin temel yapı taşı olan
[**yapay nöron**](./02-Neuron.md) kavramına geçeceğiz.

## Ayrıca Bakınız

- [Ana Sayfa](../../README.md)
- [AI nedir, ne değildir, kodla ilişkisi](00-WhatIsAI.md)
- *Öğrenme kavramı, supervised / unsupervised / reinforcement*
- &gt; [Yapay nöron, girdi/ağırlık/bias, basit örnek](./02-Neuron.md)
- [Mini neural network, hidden layer, ileri beslemeli ağ](./03-NeuralNetwork.md)
- [Ödül ve ceza, self-play, temel RL mantığı](./04-ReinforcementLearning.md)
- [Hassabis yaklaşımı, self-play, modüler mimari](./05-WhyThisArchitecture.md)
- [PingPongAI.App Gerekçeleri](./06-PingPongAI.App.md)
- [PingPongAI.App Oyun Kuralları](./07-PingPongAI.App.Rules.md)
- [Kural Tabanlı Ajan Yaklaşımı](./08-RuleBased.md)
- [AIAgent - Supervised Control Yaklaşımı](./09-AIAgent-SupervisedControl.md)
