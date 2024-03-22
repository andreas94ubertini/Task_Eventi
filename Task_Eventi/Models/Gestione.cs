using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Eventi.Models
{
    internal class Gestione
    {
        // creazione singleton
        private static Gestione? istanza;
        public static Gestione getIstanza()
        {
            if (istanza == null)
                istanza = new Gestione();

            return istanza;
        }
        private Gestione() { }
        public void Menu()
        {
            bool active = true;
            while (active)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("-1- Gestisci eventi");
                Console.WriteLine("-2- Registra partecipanti ad un evento");
                Console.WriteLine("-3- Registra risorsa per un evento");
                Console.WriteLine("==================================");

                string? input = Console.ReadLine();
                switch (input)
                {
                    case"1":
                        {
                            Console.WriteLine("-1- Aggiungi un nuovo evento");
                            Console.WriteLine("-2- Modifica un evento esistente");
                            Console.WriteLine("-3- Elimina un evento");
                            string? inputEvento = Console.ReadLine();
                            if(inputEvento == "1")
                            {
                                Console.WriteLine("Registrazione evento");
                                this.ManageEvento(false);
                            }else if(inputEvento == "2")
                            {
                                Console.WriteLine("Modifica evento");
                                this.ManageEvento(true);
                            }else if(inputEvento == "3")
                            {
                                Console.WriteLine("Modifica evento");
                                this.DeleteEvento();

                            }
                            else
                            {
                                Console.WriteLine("Comando non valido");

                            }
                            break;
                        }
                }
                
            }
        }
        private List<Evento> GetEventiList()
        {
            List<Evento> list = new List<Evento>();
            using (var db = new AccTaskEventiContext())
            {
                try
                {
                    list = db.Eventos.ToList();
                    return list;
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return list;
        }
        private void ManageEvento(bool mode)
        {
            Evento modifiedEv = new Evento();
            int convertedID = 0;
            string toAdd = "";
            if (mode == true)
            {
                toAdd = "Modifica ";
                foreach (Evento e in GetEventiList())
                {
                    Console.WriteLine($"ID: {e.EventoId}, NOME: {e.Nome}, LUOGO: {e.Luogo}, DATA: {e.DataEvento}");
                }
                Console.WriteLine("Inserisci l'ID dell'evento da modificare");

                string? idToFind = Console.ReadLine();
                convertedID = Convert.ToInt32(idToFind);
            }
            using (var db = new AccTaskEventiContext())
            {
                if (mode == true)
                {
                    modifiedEv = db.Eventos.Single(ev => ev.EventoId == convertedID);
                }
                try
                {
                    Console.WriteLine($"{toAdd}Nome evento:");
                    string? nomeEvento = Console.ReadLine();
                    Console.WriteLine($"{toAdd}Descrizione evento:");
                    string? descrEvento = Console.ReadLine();
                    Console.WriteLine($"{toAdd}Data evento:");
                    string? dataEvento = Console.ReadLine();
                    DateTime convertedDate = Convert.ToDateTime(dataEvento);
                    DateOnly dateFinal = DateOnly.FromDateTime(convertedDate);
                    Console.WriteLine($"{toAdd}Luogo evento:");
                    string? luogoEvento = Console.ReadLine();
                    Console.WriteLine($"{toAdd}capacità max evento:");
                    string? capEvento = Console.ReadLine();
                    int convertedCapEvento = Convert.ToInt32(capEvento);
                    if (mode == true)
                    {
                        modifiedEv.Nome = nomeEvento;
                        modifiedEv.Descrizione = descrEvento;
                        modifiedEv.DataEvento = dateFinal;
                        modifiedEv.Luogo = luogoEvento;
                        modifiedEv.CapMax = convertedCapEvento;
                        db.Entry(modifiedEv).State = EntityState.Modified;
                        db.SaveChanges();
                        Console.WriteLine("Evento Aggiornato");
                    }
                    else
                    {
                        Evento e = new Evento()
                        {
                            Nome = nomeEvento,
                            Descrizione = descrEvento,
                            DataEvento = dateFinal,
                            Luogo = luogoEvento,
                            CapMax = convertedCapEvento
                        };
                        db.Eventos.Add(e);
                        db.SaveChanges();
                        Console.WriteLine("Inserimento avvenuto con successo");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore: {ex.Message}");
                }
            }
        }


        private void DeleteEvento()
        {
            Console.WriteLine("Elimina un evento");
            if (GetEventiList().Count > 0)
            {
                foreach (Evento e in GetEventiList())
                {
                    Console.WriteLine($"ID: {e.EventoId}, NOME: {e.Nome}, LUOGO: {e.Luogo}, DATA: {e.DataEvento}");

                }
                Console.WriteLine("Inserisci l'ID dell'evento da eliminare");
                string? toDelete = Console.ReadLine();
                int convertedId = Convert.ToInt32(toDelete);
                using (var db = new AccTaskEventiContext())
                {
                    try
                    {
                        Evento e = db.Eventos.Single(ev => ev.EventoId == convertedId);
                        db.Eventos.Remove(e);
                        db.SaveChanges();
                        Console.WriteLine("Evento rimosso");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Nessun evento presente");
            }

        }

    }
}
