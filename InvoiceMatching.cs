using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

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
    internal class InvoiceMatching
    {
        public void 題目1()
        {
            var lottery ="132, 672, 780, 981, 220".Split(",").ToList();
            var Invoice = "222,672, 119, 431, 901, 981, 002,811".Split(",").ToList();
            var Result =new   List<string>();
            lottery = lottery.Select(x=>x.Trim()).ToList();
            Invoice = Invoice.Select(x => x.Trim()).ToList();

            lottery.ForEach(x =>    {   if(Invoice.Contains(x)) Result.Add(x);  });
            Console.WriteLine(@$"題目1");
            Console.WriteLine(@$"中獎結果是: {string.Join(",", Result)}");
        }

        public void 題目2()
        {
            var lottery = "132, 672, 780, 981, 220".Split(",").ToList();
            var Invoice = "222,001,672, 119, 431, 901, 981, 002,811".Split(",").ToList();
            var Result = new List<string>();
            lottery = lottery.Select(x => x.Trim()).ToList();
            Invoice = Invoice.Select(x => x.Trim()).ToList();

            lottery.ForEach(x => { 
                int i = Invoice.IndexOf(x);
                if (i>-1)   Result.Add(@$"{x}(第{i+1}筆)");
                                
            });
            Console.WriteLine(@$"題目2");
            Console.WriteLine(@$"中獎結果是: {string.Join(",", Result)}");
        }

        public void 題目3()
        {
 
            var Invoice = "66222,81001,52672, 10119, 85431, 95901, 06981, 77002,54321,51811, 99672".Split(",").ToList();
            var Result = new List<string>();

            Console.WriteLine(@$"題目3");
            Console.WriteLine(@$"中獎結果是: {string.Join(",", Result)}");
            Invoice = Invoice.Select(x => x.Trim()).Where(x=>x!="").ToList(); 
            Invoice.ForEach(x => {
                題目3方法1(Result,x.Length, x, Invoice.IndexOf(x));
            });

        
        }

        public void 題目3方法1 (List<string> Result, int inx, string Invoice,int reutrn_inx)
        {
            if (inx > -1 && inx>2)
            {
                int start = Invoice.Length - inx;
                var lottery = "82132, 02672, 09780, 66981, 54321,00220".Split(",").ToList();
                lottery = lottery.Select(x => x.Trim()).Select(x => x.Substring(start)).ToList();
                var tmp = Invoice.Substring(start);
                if (lottery.Contains(tmp))
                {
                    // Result.Add(@$"{Invoice}   (第筆，中獎號碼是{tmp})");
                    Console.WriteLine(@$"{Invoice}   (第{reutrn_inx+1}筆，中獎號碼是{tmp})");
                }
                else
                {
                    題目3方法1(Result,--inx, Invoice, reutrn_inx);
                }  
            }          
        }
    }

}
