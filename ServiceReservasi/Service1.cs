using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string deletePemesanan(string IDPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from dbo.Pemesanan where ID_reservasi = '" + IDPemesanan + "'";
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();
                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }



        public string editPemesanan(string IDPemesanan, string NamaCustomer, string No_Telpon)
        {
            string a = "gagal";
            try
            {
                string sql = "insert into dbo.Pemesanan set Nama_Customer = '" + NamaCustomer + "', No_Telpon =  '" + No_Telpon + "," +
                    " where ID_reservasi = '" + IDPemesanan + "' ";
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;

        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>(); //proses untuk mendeclare nama list 
            try
            {
                string sql = " select ID_reservasi, Nama_Customer, No_Telpon, " +
                    "Jumlah_pemesanan, Nama_Lokasi from dbo.Pemesanan p join dbo.Lokasi 1 on p.ID_lokasi = 1.ID_lokasi";
                connection = new SqlConnection(constring); //fungsi kkonek ke database
                com = new SqlCommand(sql, connection); // proses execute query
                connection.Open(); //membuka koneksi
                SqlDataReader reader = com.ExecuteReader(); //menampilkan datat query
                while (reader.Read())
                {
                    /* nama class */
                    Pemesanan data = new Pemesanan(); //deklarasi data, mengambil 1 persatu dari database
                    //bentuk array
                    data.IDPemesanan = reader.GetString(0); //0 itu undex, ada dikolom keberapa di string sql diatas
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesanans.Add(data); //mengumpulkan data yang awalnya dari array
                }
                connection.Close(); //untuk menutup akses ke database
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesanans;
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>(); //proses untuk mendeclare nama list yang telah dibuat dengan nama baru
            try
            {
                string sql = "select ID_lokasi, Nama_lokasi, Deskripsi_full, Kuota from dbo.Lokasi"; //declare wuery
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection); //proses execute wuery
                connection.Open(); //membuka koneksi
                SqlDataReader reader = com.ExecuteReader(); //menampilkan data query
                while (reader.Read())
                {
                    /* nama */
                    DetailLokasi data = new DetailLokasi(); //deklarasi data, mengambiil 1 persatu dari database
                    //bentuk array
                    data.IDLokasi = reader.GetString(0); //0 itu inndex. ada dikolom keberapa di string sql database
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data); //mengumpulkan data yang awalanya dari array
                }
                connection.Close(); //untuk menutup akses ke database
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }

        public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
        {
            string a = "gagal";
            try
            {
                string sql = "insert into dbo.Pemesanan values ('" + IDPemesanan + "', '" + NamaCustomer + "', '" + NoTelpon + ", " +
                    "" + JumlahPemesanan + ", '" + IDLokasi + "')"; //petik 1 untuk menyatakan varchar, petik 2 menyatakan inetegr
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                string sql2 = "update dbo.Lokasi set Kuota = Kuota - " + JumlahPemesanan + " where ID_lokasi = '" + IDLokasi + "' ";
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        string constring = "Data Source=LAPTOP-ILNEA6L8;Initial Catalog=WCFReservasi;Persist Security Info=true;User ID=sa;Password=Beauty123";
        SqlConnection connection;
        SqlCommand com; //untuk mengkoneksikan database ke visual studio
    }
}
