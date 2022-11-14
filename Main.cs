using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



class Elemek{
        public string  ev         {set;get;}
        public string  elem       {set;get;}
        public string  vegyjel    {set;get;}
        public string  rendszam   {set;get;}
        public string  felfedezo  {set;get;} 

        public Elemek(string sor)
        {
            
            var s = sor.Trim().Split(';');
            ev        = s[0];
            elem      = s[1];
            vegyjel   = s[2];
            rendszam  = s[3];
            felfedezo = s[4];
        }
    }

public class Program {

    public static bool Vegyjel(string vegy){
        if(vegy.Length > 2 || vegy.Length < 1 ){
            return false;
        }
        var abc ="ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        foreach(var x in vegy.ToUpper()){
            if(!abc.Contains(x)){
                return false; 
            }
        }
        return true;
    }

  public static void Main(string[] args) {
      var lista = new List<Elemek>();
      var f = new StreamReader("felfedezesek.csv");
      var fl = f.ReadLine();

      while(!f.EndOfStream){
          lista.Add(new Elemek(f.ReadLine()));
                  
      }
     //3.feladat
      
      Console.WriteLine($"3.feladat : Elemek száma {lista.Count}");
      
     //4.feladat
      
      var f_szam = (
          from sor in lista
          where sor.ev == "Ókor"
          select sor
      ).Count();
      Console.WriteLine($"4.feladat : Felfedezések száma a ókorban {f_szam}");

      //5.feladat 
      var keres = "";
      while(true){
          Console.Write($"5.feladat: Kérek egy vegyjelet: ");
          keres = Console.ReadLine().ToUpper();
          if(Vegyjel(keres)){
              break;
              
          }
      }

      var vjel =(
          from sor in lista
          where sor.vegyjel == keres
          select sor
      );

      //6.feladat
      var kereses = (
          from sor in lista
          where sor.vegyjel.ToUpper() == keres
          select sor            
      );
      Console.WriteLine($"6.feladat: Keresés");
      if (kereses.Any())
      {
          var a = kereses.First();
          Console.WriteLine($"\t\t Az elem vegyjele: {a.vegyjel} \n \t\t Az elem neve: {a.elem} \n \t\t Rendszáma: {a.rendszam} \n\t\t Felfedezés éve: {a.ev} \n \t\t Felfedező: {a.felfedezo}" );
      }
      else{
          Console.WriteLine($"\t Nincs ilyen elem az adatforrásban");
      }
      //7.feladat
      var evek = (
          from sor in lista
          where sor.ev != "Ókor"
          select int.Parse(sor.ev)
      ).ToList();

      var klista = new List<int>();
      for(int i = 0 ; i < evek.Count()-1 ; i++){
          klista.Add(evek[i+1]-evek[i]);
      }
      Console.WriteLine($"7.feladat: {klista.Max()} év volt a leghosszabb időszak két elem között! ");

      //8.feladat
      var nokor = (
          from sor in lista
          where sor.ev != "Ókor"
          group sor by sor.ev
      );
      var stat = (
          from sor in nokor
          where sor.Count() > 3
          select sor
      );
      Console.WriteLine($"8.feladat: Statisztika");
      foreach(var s in stat){
          Console.WriteLine($"\t\t {s.Key}: {s.Count()} db");
      }
      



          
  }
}