## LINQ运算符
  
### 一、筛选  
| 方法 | 描述 
| ---- | ---- 
| Where | 返回满足给定条件得元素集合 
| Take | 返回前count个元素，丢弃剩余得元素 
| Skip | 跳过前count个元素，返回剩余得元素
| TakeWhile | 返回输入序列中的元素，直到断言判断为false
| SkipWhile | 持续忽略输入序列中的元素，直到断言为false，返回剩余的元素
| Distinct | 返回一个没有重复元素的集合(去重)

1. **Where运算符**
```
        string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

        //Result: { "Harry", "Mary", "Jay" }
        IEnumerable<string> query = names.Where(name => name.EndsWith("y"));

        //查询表达式
        IEnumerable<string> query1 = from name in names
                                        where name.EndsWith("y")
                                        select name;

        //配合let、order by 和join子句，可以多次使用where
        IEnumerable<string> query3 = from name in names
                                        where name.Length > 3
                                        let u = name.ToUpper()
                                        where u.EndsWith("Y")
                                        select u;

        //索引筛选。 第二个可选类型参数用于指定输入序列中每个元素的位置，并可以用于元素的筛选。在EF或者LINQ to SQL中使用索引筛选会抛出异常
        //下面例子中，查询会跳过偶数位置的元素。 Result: { "Tom", "Harry", "Jay" }
        IEnumerable<string> query4 = names.Where((n, i) => i % 2 == 0);       
```

2.**Take和Skip运算符**
```
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            //Result: { "Tom", "Dick" }
            IEnumerable<string> query = names.Take(2);

            //Result: { "Harry", "Mary", "Jay" }
            IEnumerable<string> query1 = names.Skip(2);

            //组合使用，可用于分页查询。查询第3个到第4个结果 Result: { "Harry", "Mary" }
            IEnumerable<string> query2 = names.Skip(2).Take(2);
```

3. **TakeWhile和SkipWhile运算符**
```
            int[] numbers = { 3, 5, 2, 234, 4, 1 };            

            //Result: { 3,5,2 }
            IEnumerable<int> query = numbers.TakeWhile(n => n < 100);            
            
            //Result: { 234,4,1 }
            IEnumerable<int> query1 = numbers.SkipWhile(n => n < 100);            
```

4. **Distinct运算符**
```
            //Result: HelloWrd
            char[] distinctLetters = "HelloWorld".Distinct().ToArray();
            string s = new string(distinctLetters);
```


### 二、映射  
| 方法 | 描述 
| ---- | ---- 
| Select | 将输入中的每一个元素按照给定Lamdba表达式进行转换 
| SelectMany | 将输入的每一个元素按照Lamdba表达式进行转换，并将嵌套的集合展平后连接在一起  
  
1. **Select运算符**
```
            List<Person> persons = new List<Person> {  
                new Person { Name = "Tom", Age = 22, Sex = "Men" }, 
                new Person { Name = "Dick", Age = 24, Sex = "Men" }, 
                new Person { Name = "Harry", Age = 25, Sex = "Woman" },
                new Person { Name = "Mary", Age = 26, Sex = "Woman" },
                new Person { Name = "Jay", Age = 27, Sex = "Men" }
            };

            //映射Name
            IEnumerable<string> query = persons.Select(p => p.Name);
            //与上面等价
            IEnumerable<string> query1 = from p in persons
                                        select p.Name;

            //有时查询语法的映射不会执行任何转换，仅仅是为了满足查询必须以select结尾的语法要求。
            IEnumerable<Person> query3 = from p in persons
                                         where p.Sex == "Men"
                                         select p;

            //索引映射
            IEnumerable<string> query4 = persons.Select((p, i) => i + ":" + p.Name);
```

2. **SelectMany**
```
            var fullNames = new[] { "Anne Williams", "John Fred Smith", "Sue Green" }.AsQueryable();

            //展开子序列，将嵌套的集合平面化
            IEnumerable<string> query1 = fullNames.SelectMany(name => name.Split());
            
            //和上面是等效的
            IEnumerable<string> query2 = from fullName in fullNames
                                         from name in fullName.Split()
                                         select name;
            foreach (string s in query1) Console.WriteLine(s);
```

### 三、映射  
| 方法 | 描述 
| ---- | ---- 
| Join | 使用查找规则对两个集合的元素进行匹配，返回平面的结果集
| GroupJoin | 同上，但是返回层次化的结果集  
| Zip | 同时遍历两个集合中的元素(像拉链一样)，返回经过处理的元素对。  