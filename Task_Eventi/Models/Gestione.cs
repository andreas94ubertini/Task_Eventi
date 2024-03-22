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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ____    ___   _____ ______  ____   ___   ____     ___        ___  __ __    ___  ____   ______  ____ \n /    |  /  _] / ___/|      ||    | /   \\ |    \\   /  _]      /  _]|  |  |  /  _]|    \\ |      ||    |\n|   __| /  [_ (   \\_ |      | |  | |     ||  _  | /  [_      /  [_ |  |  | /  [_ |  _  ||      | |  | \n|  |  ||    _] \\__  ||_|  |_| |  | |  O  ||  |  ||    _]    |    _]|  |  ||    _]|  |  ||_|  |_| |  | \n|  |_ ||   [_  /  \\ |  |  |   |  | |     ||  |  ||   [_     |   [_ |  :  ||   [_ |  |  |  |  |   |  | \n|     ||     | \\    |  |  |   |  | |     ||  |  ||     |    |     | \\   / |     ||  |  |  |  |   |  | \n|___,_||_____|  \\___|  |__|  |____| \\___/ |__|__||_____|    |_____|  \\_/  |_____||__|__|  |__|  |____|\n                                                                                                      ");

            bool active = true;
            while (active)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("-1- Gestisci eventi");
                Console.WriteLine("-2- Gestisci partecipanti");
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
                    case "2":
                        {
                            Console.WriteLine("-1- Aggiungi un nuovo Partecipante");
                            Console.WriteLine("-2- Modifica un Partecipante esistente");
                            Console.WriteLine("-3- Elimina un Partecipante");
                            string? inputPartecipante = Console.ReadLine();
                            if (inputPartecipante == "1")
                            {
                                Console.WriteLine("Registrazione partecipante");
                                this.ManagePartecipante(false);
                            }
                            else if (inputPartecipante == "2")
                            {
                                Console.WriteLine("Modifica partecipante");
                                this.ManagePartecipante(true);
                            }
                            else if (inputPartecipante == "3")
                            {
                                Console.WriteLine("Elimina partecipante");
                                this.DeletePartecipante();

                            }
                            else
                            {
                                Console.WriteLine("Comando non valido");

                            }
                            break;
                        }
                    case "3": {
                            Console.WriteLine("-1- Aggiungi una nuova Risorsa");
                            Console.WriteLine("-2- Elimina una Risorsa");
                            string? inputRis = Console.ReadLine();
                            if (inputRis == "1")
                            {
                                Console.WriteLine("Registrazione risorsa");
                                this.ManageRisorsa();
                            }
                            else if (inputRis == "2")
                            {
                                Console.WriteLine("Elimina risorsa");
                                this.ManagePartecipante(true);
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

        #region Eventi
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


            }
            using (var db = new AccTaskEventiContext())
            {
                try
                {
                    if (mode == true)
                    {
                        string? idToFind = Console.ReadLine();
                        convertedID = Convert.ToInt32(idToFind);
                        modifiedEv = db.Eventos.Single(ev => ev.EventoId == convertedID);
                    }
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
        #endregion

        #region Partecipanti

        private List<Partecipante> GetPartecipanti()
        {
            List<Partecipante> list = new List<Partecipante>();
            using (var db = new AccTaskEventiContext())
            {
                    list = db.Partecipantes.ToList();

            }
            return list;
        }
        private void ManagePartecipante(bool mode)
        {
            Partecipante modifiedP = new Partecipante();
            int convertedID = 0;
            string toAdd = "";

            using (var db = new AccTaskEventiContext())
            {

                if (mode == true)
                {

                    if (db.Partecipantes.ToList().Count > 0)
                    {
                        foreach (Partecipante p in GetPartecipanti())
                        {
                            Console.WriteLine($"ID: {p.PartecipanteId}, NOME: {p.Nome}, COGNOME: {p.Cognome}, Cf: {p.CodFis}");
                        }
                        Console.WriteLine("Inserisci l'ID del partecipante da modificare");
                    }


                }
                try
                {

                    if (mode == true)
                    {
                        toAdd = "Modifica ";

                        string? idToFind = Console.ReadLine();
                        convertedID = Convert.ToInt32(idToFind);
                        modifiedP = db.Partecipantes.Single(par => par.PartecipanteId == convertedID);
                    }
                    Console.WriteLine($"{toAdd}Nome partecipante:");
                    string? nome = Console.ReadLine();
                    Console.WriteLine($"{toAdd}Cognome partecipante:");
                    string? cognome = Console.ReadLine();
                    Console.WriteLine($"{toAdd}Contatto partecipante:");
                    string? contatto = Console.ReadLine();
                    Console.WriteLine($"{toAdd}Codice Fiscale partecipante:");
                    string? codFis = Console.ReadLine();
                    Console.WriteLine($"{toAdd}Evento participante:");
                    foreach (Evento e in GetEventiList())
                    {
                        Console.WriteLine($"ID: {e.EventoId}, NOME: {e.Nome}, LUOGO: {e.Luogo}, DATA: {e.DataEvento}");
                    }
                    Console.WriteLine("Inserisci l'ID dell'evento da aggiungere");
                    string? eventoRif = Console.ReadLine();
                    convertedID = Convert.ToInt32(eventoRif);
                    if (mode == true)
                    {
                        modifiedP.Nome = nome;
                        modifiedP.Cognome = cognome;
                        modifiedP.Contatto = contatto;
                        modifiedP.CodFis = codFis;
                        modifiedP.EventoRif = convertedID;
                        db.Entry(modifiedP).State = EntityState.Modified;
                        db.SaveChanges();
                        Console.WriteLine("Partecipante Aggiornato");
                    }
                    else
                    {
                        Partecipante p = new Partecipante()
                        {
                            Nome = nome,
                            Cognome = cognome,
                            Contatto = contatto,
                            CodFis = codFis,
                            EventoRif = convertedID
                    };
                        db.Partecipantes.Add(p);
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
        private void DeletePartecipante()
        {
            if (GetPartecipanti().Count > 0)
            {
                foreach (Partecipante p in GetPartecipanti())
                {
                    Console.WriteLine($"ID: {p.PartecipanteId}, NOME: {p.Nome}, COGNOME: {p.Cognome}, Cf: {p.CodFis}");
                }
                Console.WriteLine("Inserisci l'ID del partecipante da eliminare");
                string? toDelete = Console.ReadLine();
                int convertedId = Convert.ToInt32(toDelete);
                using (var db = new AccTaskEventiContext())
                {
                    try
                    {
                        Partecipante p = db.Partecipantes.Single(pa => pa.PartecipanteId == convertedId);
                        db.Partecipantes.Remove(p);
                        db.SaveChanges();
                        Console.WriteLine("Partecipante rimosso");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Nessun partecipante presente");
            }
        }


        #endregion

        #region Risorse

        private List<Risorsa> GetRisorseListByEv(int evID)
        {
            List<Risorsa> list = new List<Risorsa>();
            using(var db = new AccTaskEventiContext())
            {
                try
                {
                    Evento e = db.Eventos.Include(ev => ev.Risorsas).Single(ev => ev.EventoId == evID);
                    list = e.Risorsas.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return list;
        }
        private void ManageRisorsa()
        {
            foreach (Evento e in GetEventiList())
            {
                Console.WriteLine($"ID: {e.EventoId}, NOME: {e.Nome}, LUOGO: {e.Luogo}, DATA: {e.DataEvento}");
            }
            Console.WriteLine("Inserisci l'ID dell'evento per aggiungere una risorsa");
            string? evID = Console.ReadLine();
            using(var db =new AccTaskEventiContext())
            {
                try
                {
                    int convertedEvID = Convert.ToInt32(evID);
                    Console.WriteLine("Tipo di risorsa");
                    string? tipo = Console.ReadLine();
                    Console.WriteLine("Qt risorsa");
                    string? qt = Console.ReadLine();
                    int convertedQt = Convert.ToInt32(qt);
                    Console.WriteLine("Fornitore risorsa");
                    string? fornitore = Console.ReadLine();
                    Console.WriteLine("Prezzo della risorsa");
                    string? prezzo = Console.ReadLine();
                    decimal convertedPrez = Convert.ToDecimal(prezzo);
                    
                    Risorsa r = new Risorsa()
                    {
                        Tipo = tipo,
                        Costo = convertedPrez,
                        Fornitore = fornitore,
                        Qt = convertedQt,
                        EventoRif = convertedEvID
                    };
                    db.Risorsas.Add(r);
                    db.SaveChanges();
                    Console.WriteLine("Risorsa aggiunta");
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        private void DeleteRisorsa()
        {
            foreach (Evento e in GetEventiList())
            {
                Console.WriteLine($"ID: {e.EventoId}, NOME: {e.Nome}, LUOGO: {e.Luogo}, DATA: {e.DataEvento}");
            }
            Console.WriteLine("Inserisci l'ID dell'evento per eliminare una risorsa");
            string? evID = Console.ReadLine();
            using (var db = new AccTaskEventiContext())
            {
                try
                {
      
                    int convertedEvID = Convert.ToInt32(evID);
                    Console.WriteLine("Inserisci ID della risorsa da eliminare");
                    string? risorsaID = Console.ReadLine();
                    int convertedRisID = Convert.ToInt32(risorsaID);
                    foreach (Risorsa r in GetRisorseListByEv(convertedEvID))
                    {
                        Console.WriteLine($"ID: {r.RisorsaId}, TIPO: {r.Tipo}, Qt: {r.Qt}, PREZZO: {r.Costo}, FORNITORE: {r.Fornitore}");
                    }
                    Risorsa toDelete = db.Risorsas.Single(r => r.RisorsaId == convertedRisID);
                    db.Risorsas.Remove(toDelete);
                    db.SaveChanges();
                    Console.WriteLine("Risorsa Eliminata");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        #endregion
    }
}
