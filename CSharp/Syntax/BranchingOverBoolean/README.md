# Branching Over Boolean Tests

## Where is it come from

此篇來自於 [Zoran Horvat](http://app.pluralsight.com/author/zoran-horvat)於[PluralSight的影片 Making Your C# Code More Object-oriented](http://app.pluralsight.com/courses/c-sharp-code-more-object-oriented)
的章節 Rendering Branching Over Boolean Tests Obsolete 的實作練習,感覺作法很有意思,而且符合各項近期的程式碼撰寫原則,也許我理解概念上可能有誤
如有發現請多多指教

## Expect

將長篇的Bool判斷及其相關後續邏輯 以SOLID的原則加以分解
並嘗試表現此作法與SOLID所帶來的好處 ex: 擴充性

## A liite Difficult to Understand

這個做法與邏輯走向並不像之前那樣子的一眼就可以看完,而事實上過去的做法在判斷越來越多的狀況下也沒辦法一眼就看完,甚至會衍生出判斷理解錯誤的問題
在這裡你可能要先有幾個觀念,會比較容易理解
 - IoC
 - Lambda
 - Design Pattern - State, Facade, Strategy, Command
 - SOLID

## Extensions

此做法可將各部分以IoC Container加以運用
就可以很明確地感覺到Container 所帶來的優勢

## Weak Point

實際的code變多 and 比較不直覺
不直覺的部分只要了解其運作其實會變得相對簡單
code的部分視情況可以由IDE 或 Code Generator 輔助

## Where I Can Find This Kind Of Pattern

這類型的做法在Api or Framework 或是 具備複雜判斷邏輯或動態邏輯的程式中比較常見

## History

 - 2016/11/08 First Release without Unit Test

## Credits

Kevin Cheng
