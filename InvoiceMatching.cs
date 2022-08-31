using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;


namespace Topic.Invoice
{
    /// <summary>
    /// 今天看到一個題目(其實有三個子題), 就自己練習了一下, 有興趣的也可以寫看看,先寫第一題
    /*
        第一小題
            比對發票是否中獎: 中獎號碼為 132, 672, 780, 981, 220; 
            您手邊的發票號碼為:222,672, 119, 431, 901, 981, 002,811
            中獎結果是: 672, 981

        第二小題
            比對發票是否中獎: 中獎號碼為 132, 672, 780, 981, 220; 
            您手邊的發票號碼為:222,001,672, 119, 431, 901, 981, 002,811
            中獎結果是: 672(第3筆), 981(第7筆)

        第三小題
            比對發票是否中獎,號碼有五位數, 只要最後3,4,5碼相同,都算中獎:
            中獎號碼為 82132, 02672, 09780, 66981, 54321,00220;假設末三碼都不會相同
             您手邊的發票號碼為:
                        66222,81001,52672, 10119, 85431, 95901, 06981, 77002,54321,51811, 99672
            中獎結果是: 
	            52672(第3筆, 中獎號碼是2672), 
	            06981(第7筆, 中獎號碼是981) , 
	            54321(第8筆, 中獎號碼是54321) ,
	            99672(第 11 筆, 中獎號碼是 02672)
    */
    /// </summary>
    public class InvoiceMatching
    {
        public void 題目1()
        {
            var Lottery = "132, 672, 780, 981, 220".Split(",").Select(x => x.Trim()).ToList();
            var Invoice = "222,672, 119, 431, 901, 981, 002,811".Split(",").Select(x => x.Trim()).ToList();
            var Result = new List<string>();

            Console.WriteLine("題目1 中獎結果是\n");

            // 方法1
            Lottery.ForEach(x => { if (Invoice.Contains(x)) Result.Add(x); });
            Console.WriteLine($"\t方法1： {string.Join(",", Result)}");

            // 方法2 -  參考郭老師
            var data = Invoice.Intersect(Lottery);
            Console.WriteLine($"\t方法2： {string.Join(",", data.ToList())}");
        }

        public void 題目2()
        {
            var Lottery = "132, 672, 780, 981, 220".Split(",").Select(x => x.Trim()).ToList();
            var Invoice = "222,001,672, 119, 431, 901, 981, 002,811".Split(",").Select(x => x.Trim()).ToList();
            var Result = new List<string>();

            Console.WriteLine("題目2 中獎結果是\n");
            // 方法1
            Lottery.ForEach(x =>
            {
                int i = Invoice.IndexOf(x);
                if (i > -1) Result.Add($"{x}(第{i + 1}筆)");
            });
            Console.WriteLine("  方法1：");
            Console.WriteLine($"\t{string.Join("\n\t", Result)}");

            // 方法2 -  參考郭老師 ，使用Intersect 
            var data2 = Lottery.Intersect(Invoice)
                .Select(x => $"{x}(第{Invoice.IndexOf(x) + 1}筆)").ToList();
            Console.WriteLine("  方法2：");
            Console.WriteLine($"\t{string.Join("\n\t", data2)}");

            // 方法3 -  參考郭老師， 換成使用IntersectBy
            var data3 =
                Invoice.Select((x, inx) => new { numbers = x, inx = inx + 1 })
                .IntersectBy(Lottery.Select(x => x), y => y.numbers)
                .Select(z => $"{z.numbers}(第{z.inx}筆)");

            Console.WriteLine("  方法3：");
            Console.WriteLine($"\t{string.Join("\n\t", data3)}");

            // 方法4 - 純紀錄學習， 抄寫雪岳
            var bonusMapper  = Lottery.Select((s,index)=>new Invoice() { NOIndex=index, NO=s });
            var recepitMapper = Invoice.Select((s, index) => new Invoice() { NOIndex = index, NO = s });
            var data4=recepitMapper.Intersect(bonusMapper,new InvoiceComparer())
                .Select(x=>x.ToString());

            Console.WriteLine("  方法4：");
            Console.WriteLine($"\t{string.Join("\n\t", data4)}");
        }

 
        public void 題目3()
        {
            var sw = new Stopwatch();
            var Invoice = "66222,81001,52672, 10119, 85431, 95901, 06981, 77002,54321,51811, 99672".Split(",").Where(x => x != "").Select(x => x.Trim()).ToList();
            var Lottery = "82132, 02672, 09780, 66981, 54321,00220".Split(",").Where(x => x.Trim() != "").Select(x => x.Trim()).ToList();
           
            Console.WriteLine("題目3 中獎結果是\n");
            // 方法1
            var Result = new List<string>();
            sw.Start();
            Invoice.ForEach(x =>
            {
                題目3方法1(Lottery,Result, x.Length, x, Invoice.IndexOf(x));
            });
            sw.Stop();

            Console.WriteLine($"  方法1 花費時間： {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"\t{string.Join("\n\t", Result)}");
 
   
        }

        public void 題目3方法1(List<string> Lottery, List<string> Result, int inx, string Invoice, int reutrn_inx)
        {     
            if (inx > 2)
            {
                int start = Invoice.Length - inx;
                var match = Invoice.Substring(start);                
                var tmpLottery  = Lottery.Select(x=>x.Substring(start)).ToList();
                if (tmpLottery.Contains(match))
                    Result.Add($"{Invoice}   (第{reutrn_inx + 1}筆，中獎號碼是 {match})");
                else
                    題目3方法1(Lottery, Result, --inx, Invoice, reutrn_inx);
            }
        }

    }

    public class InvoiceQ3
    {      
        public string Value { get; set; }
        public int Idx { get; set; }
        public string result { set;get;}
    }

    public class InvoiceQ3Comparer : IEqualityComparer<InvoiceQ3>
    {
        public bool Equals(InvoiceQ3 x, InvoiceQ3 y)
        {          
            return x.Value == y.Value;
        }

        public int GetHashCode(InvoiceQ3 obj)
        {
            return obj.Value.GetHashCode();
        }
    }



    public class Invoice
    {
        public  int NOIndex { set;get;}
        public string NO { set; get; }        
        public override string ToString()
        { 
            return $"{NO} (第 {NOIndex+1} 筆)";
        }
    }

    public class InvoiceComparer:IEqualityComparer<Invoice>
    {
        public bool Equals(Invoice x, Invoice y)
        {
            return x.NO==y.NO;
        }

        public int GetHashCode(Invoice obj)
        {
            return obj.NO.GetHashCode();
        }
    }
}
