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
        string constring = "Data Source=LAPTOP-ILNEA6L8;Initial Catalog=WCFReservasi;Persist Security Info=True; User ID=sa;password=Beauty123";
        SqlConnection connection;
        SqlCommand cmd;


        public string Login(string username, string password)
        {
            string kategori = "";

            string sql = "select Kategori from Login where Username = '" + username + "' and Password = '" + password + "'";
            connection = new SqlConnection(constring);
            cmd = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                kategori = reader.GetString(0);
            }

            return kategori;
        }

        public string Register(string username, string password, string kategori)
        {
            try
            {
                string sql = "insert into Login values('" + username + "', '" + password + "', '" + kategori + "')";
                connection = new SqlConnection(constring);
                cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return "Sukses";
            }

            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string UpdateRegister(string username, string password, string kategori, int id)
        {
            try
            {
                string sql2 = "update Login SET username = '" + username + "', Password = '" + password + "', Kategori = '" + kategori + "' where ID_Login = '" + id + "'";
                connection = new SqlConnection(constring);
                cmd = new SqlCommand(sql2, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return "Sukses";
            }

            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DeleteRegister(string username)
        {
            try
            {
                int id = 0;
                string sql = "select ID_login from dbo.Login where Username = '" + username + "'";
                connection = new SqlConnection(constring);
                cmd = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }

                connection.Close();
                string sql2 = "delete from Login where ID_login= '" + id + "'";
                cmd = new SqlCommand(sql2, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return "Sukses";
            }

            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public List<DataRegister> DataRegist()
        {
            List<DataRegister> list = new List<DataRegister>();
            try
            {
                string sql = "select ID_login, Username, Password, Kategori from dbo.Login";
                connection = new SqlConnection(constring);
                cmd = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DataRegister data = new DataRegister();
                    data.id = reader.GetInt32(0);
                    data.username = reader.GetString(1);
                    data.password = reader.GetString(2);
                    data.kategori = reader.GetString(3);
                    list.Add(data);
                }

                connection.Close();
            }

            catch (Exception e)
            {
                e.ToString();
            }

            return list;
        }


        public string deletePemesanan(string Idpemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from dbo.Pemesanan where ID_reservasi = '" + Idpemesanan + "'";
                connection = new SqlConnection(constring);
                cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>();
            try
            {
                string sql = "select ID_lokasi, Nama_lokasi, Deskripsi_full, Kuota from dbo.Lokasi";
                connection = new SqlConnection(constring);
                cmd = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DetailLokasi data = new DetailLokasi();
                    data.IDLokasi = reader.GetString(0);
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;

        }



        public string editPemesanan(string IDPemesanan, string NamaCustomer, string NoTelepon)
        {
            string a = "gagal";
            try
            {
                string sql = "update dbo.Pemesanan set Nama_customer = '" + NamaCustomer + "', No_telepon = '" + NoTelepon + "'" +
                    " where ID_reservasi = '" + IDPemesanan + "' ";
                connection = new SqlConnection(constring);
                cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }





        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>();
            try
            {
                string sql = "select ID_reservasi, Nama_customer, No_telepon, Jumlah_pemesanan, Nama_lokasi from dbo.Pemesanan p join dbo.Lokasi l on p.ID_lokasi = l.ID_lokasi";
                connection = new SqlConnection(constring);
                cmd = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pemesanan data = new Pemesanan();
                    data.IDPemesanan = reader.GetString(0);
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesanans.Add(data);
                }
                connection.Close();
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

        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelepon, int JumlahPemesanan, string IDLokasi)
        {
            string a = "gagal";

            try
            {
                string sql = "INSERT INTO dbo.Pemesanan VALUES('" + IDPemesanan + "','" + NamaCustomer + "','" + NoTelepon + "','" + JumlahPemesanan + "','" + IDLokasi + "')";
                connection = new SqlConnection(constring);
                cmd = new SqlCommand(sql, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                a = "Sukses";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return a;
        }
    }
}
