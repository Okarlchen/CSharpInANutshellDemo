### LINQ  

1. LINQ支持查询任何实现了**IEnumerable\<T>*接口的集合类型，无论是数组、列表还是XML DOM  

2. 最简单的查询：用Where运算符找出一个数组中字符个数至少为4的元素
```
            //数组类型继承了IEnumerable，因此可以使用LINQ查询
            string[] names = { "Tom", "Dick", "Harry","Mary","Jay" };

            IEnumerable<string> filteredNames1 = System.Linq.Enumerable.Where(names, n => n.Length >= 4);

            //使用静态扩展方法
            IEnumerable<string> filteredNames2 = names.Where(n => n.Length >= 4);

            //使用var隐式声明filteredNames3的类型，进一步精简代码
            var filteredNames3 = names.Where(n => n.Length >= 4);
```

3. 查询表达式，和上面的语句等效
```         
            //查询表达式
            var filteredNames4 = from n in names
                                where n.Length >= 4
                                select n;
```
3. 查询运算符链: 从数组中找出所有包含字母"a"的字符串，并将其按照长度进行排序，然后将结果转换为大写
```
            //查询运算符链
            IEnumerable<string> query = names
                .Where(n => n.Contains("a"))
                .OrderBy(n => n.Length)
                .Select(n => n.ToUpper());
```