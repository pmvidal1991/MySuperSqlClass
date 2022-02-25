﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MySuperSqlClass
{
    public class MySuperSqlLib
    {
        private String ConString;
        private SqlConnection con;
        private string[] selectOptions = new string[] { "Select", "select", "SELECT" };
        private string[] insertOptions = new string[] { "Insert", "insert", "INSERT" };


        public MySuperSqlLib(string con)
        {

            this.ConString = con;
            this.con = new SqlConnection(ConString);

        }
        public SqlConnection getCon()
        {
            return this.con;
        }
        public bool testcon()
        {

            bool y = false;
            try
            {
                this.con.Open();
                y = true;
                this.con.Close();
            }
            catch (SqlException ex)
            {
                this.con.Close();
                y = false;
            }
            return y;
        }

        public void closcon()
        {
            this.con.Close();

        }
        public void opencon()
        {
            this.con.Open();
        }
        public List<T> ExecuteSelectQuery<T>(string query, Dictionary<string, string> parameters) where T : new()
        {


            try
            {
                bool errors = false;
                string op = query.Split(" ")[0];
                if (op != selectOptions[0] && op != selectOptions[1] && op != selectOptions[2])
                {
                    errors = true;
                }
                if (errors)
                {
                    throw new Exception("This method only support Select querys");
                }
                SqlCommand command = new SqlCommand(query, this.getCon());
                foreach (KeyValuePair<string, string> kvp in parameters)
                {
                    command.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }
                opencon();
                SqlDataReader reader2 = command.ExecuteReader();
                List<T> list = new List<T>();
                while (reader2.Read())// Caso exista dados , ele seta o objeto do tipo "Users" com os dados que vêm do base de dados
                {

                    T tmp = new T();
                    var typeProperties = typeof(T).GetProperties();
                    foreach (var property in typeProperties)
                    {
                        property.SetValue(tmp, reader2[property.Name], null);

                    }
                    list.Add(tmp);

                }
                reader2.Close();
                closcon();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExecuteQuery(string query, Dictionary<string, string> parameters)
        {


            try
            {
                bool errors = false;
                string op = query.Split(" ")[0];
                if (op == selectOptions[0] || op == selectOptions[1] || op == selectOptions[2])
                {
                    errors = true;
                }
                if (errors)
                {
                    throw new Exception("This method does not support Select querys");
                }
                SqlCommand command = new SqlCommand(query, this.getCon());
                foreach (KeyValuePair<string, string> kvp in parameters)
                {
                    command.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }
                opencon();
                command.ExecuteNonQuery();
                closcon();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ExecuteInsertQueryReturningId(string query, Dictionary<string, string> parameters)
        {


            try
            {
                bool errors = false;
                string op = query.Split(" ")[0];
                if (op != insertOptions[0] && op != insertOptions[1] && op != insertOptions[2])
                {
                    errors = true;
                }
                if (errors)
                {
                    throw new Exception("This method only support Insert querys");
                }
                SqlCommand command = new SqlCommand(query, this.getCon());
                foreach (KeyValuePair<string, string> kvp in parameters)
                {
                    command.Parameters.AddWithValue(kvp.Key, kvp.Value);
                }
                opencon();
                int modified = Convert.ToInt32(command.ExecuteScalar());
                closcon();

                return modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
