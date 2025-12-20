# Neural Network (Yapay Sinir Ağı)

Bu doküman, tek bir yapay nörondan çoklu nöron yapısına geçişi anlatır.

## Amaç

- matematik öğretmek değil
- neural network fikrini sezgisel olarak oturtmak
- öğrenmenin neden tek bir nöronla mümkün olmadığını göstermek

## Tek Nöronun Sınırı

[Bir önceki dokümanda](./02-Neuron.md) gördük ki:

- tek bir nöron
- girdileri alır
- ağırlıklarla çarpar
- bir çıktı üretir

Bu yapı:

- basit kararlar için yeterlidir
- doğrusal ilişkileri ayırt edebilir

Ancak gerçek dünyadaki problemler:

- çok boyutludur
- doğrusal değildir
- birden fazla özelliğin birlikte değerlendirilmesini gerektirir

Bu noktada **ağ fikri** ortaya çıkar.

## Neural Network Nedir?

Bir neural network:

- birden fazla yapay nörondan oluşur
- bu nöronlar katmanlar halinde düzenlenir
- her katman, bir önceki katmanın çıktısını girdi olarak alır

Önemli nokta şudur:

> Zeka tek bir nöronda değil, nöronların birlikte çalışmasındadır.

## Katman Kavramı (Layers)

Bir neural network genellikle üç temel katmandan oluşur.

### 1. Girdi Katmanı (Input Layer)

Girdi katmanı:

- dış dünyadan gelen ham verileri alır
- hesaplama yapmaz
- karar vermez
- sadece veriyi ağa taşır

Ping-pong oyunu için örnek girdiler:

- topun x koordinatı
- topun y koordinatı
- topun x yönündeki hızı
- topun y yönündeki hızı
- raketin y konumu

### 2. Gizli Katmanlar (Hidden Layers)

Asıl öğrenme burada gerçekleşir.

Gizli katmanlar:

- dışarıdan doğrudan gözlemlenemez
- insan tarafından anlamlandırılamaz
- soyut temsiller üretir

Burada:

- ⚡ sezgi oluşur
- 🔥 ama bilinç oluşmaz

Bu katmanlar:

- örüntüleri yakalar
- ilişkileri keşfeder
- karar sınırlarını şekillendirir

### 3. Çıkış Katmanı (Output Layer)

Çıkış katmanı:

- ağın verdiği nihai kararı temsil eder
- sayısal değerler üretir
- bu değerler eyleme dönüştürülür

Ping-pong için örnek çıktılar:

- yukarı hareket et
- aşağı hareket et
- sabit kal

Neural network için bunların hiçbiri bir anlam taşımaz.\
Bunlar sadece sayıdır.

## Bilgi Ağ İçinde Nasıl Akar?

Bilgi akışı:

- girdi katmanından başlar
- gizli katmanlardan geçer
- çıkış katmanında sonlanır

Bu sürece **ileri besleme (feed-forward)** denir.

Her adımda:

- ağırlıklar etkili olur
- aktivasyon fonksiyonları devreye girer
- değerler dönüştürülür

Ağ:

- kural bilmez
- amaç bilmez
- sadece matematiksel dönüşümler yapar

## Neden Derinlik Önemlidir?

Katman sayısı arttıkça:

- temsil gücü artar
- daha karmaşık örüntüler yakalanabilir
- soyutlama seviyesi yükselir

Ancak:

- her problem derin ağ gerektirmez
- basit problemler için basit ağlar daha etkilidir

Derinlik:

- bir güçtür
- ama beraberinde maliyet getirir

## Neural Network Ne Değildir?

Bu bölüm özellikle net olmalı.

Neural network:

- düşünmez
- anlamaz
- 🔥 bilinçli değildir
- niyet taşımaz
- ✨ sihir değildir

Yaptığı tek şey şudur:

- sayıları alır
- sayısal dönüşümler uygular
- sayılar üretir

## PingPongAI Bağlamı

Bu projede neural network:

- oyunu bilmez
- kuralları bilmez
- kazanmayı bilmez

Sadece:

- durumları gözlemler
- çıktılar üretir
- sonuçlara göre değişir

Öğrenme:

- bir sonraki aşamada
- ödül ve ceza ile gerçekleşir

Bir neural network tek başına öğrenemez.

Öğrenme için:

- geri bildirim
- ödül
- ceza

gerekir.

Bir sonraki dokümanda, neural network öğrenimi için [**ödül ve ceza**](./04-ReinforcementLearning.md) yapısının nasıl oluştuğunu ele alacağız.

## Ayrıca Bakınız

- [Ana Sayfa](../../README.md)
- [AI nedir, ne değildir, kodla ilişkisi](00-WhatIsAI.md)
- [Öğrenme kavramı, supervised / unsupervised / reinforcement](./01-WhatIsLearning.md)
- [Yapay nöron, girdi/ağırlık/bias, basit örnek](./02-Neuron.md)
- *Mini neural network, hidden layer, ileri beslemeli ağ*
- &gt; [Ödül ve ceza, self-play, temel RL mantığı](./04-ReinforcementLearning.md)
- [Hassabis yaklaşımı, self-play, modüler mimari](./05-WhyThisArchitecture.md)
