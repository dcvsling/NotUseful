# Generic Controller 泛型控制器

## Why Generic Controller

預設的Controller 通常不能宣告為 Open Generic Type
如果使用Open Generic Type的話
會直接從待選清單中剃除
而為什麼我會想實作 Generic Controller
理由其實很簡單 
因為我不想每一個Entity or UnitOfWork都寫一個Controller
其背後的原因過於攏長 有興趣的話可以另外找我討論

## Expect

透過在RouteAttribute的 Template上宣告"[generic]"
來找到正確的controller執行

## Constraint

1. 此Solution為DotNet Core Mvc的實作方式 可能與DotNetFramework不同<br/>
不過概念基本上不會差太多<br/> 
可以於HttpConfiguration中替換Service的實作來調整
2. 此做法必須限制泛型的約束條件為特定基礎類別

## History

2016/10/14 first release

## Credits

Kevin Cheng
