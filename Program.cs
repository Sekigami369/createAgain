using System;
using Microsoft.Data.SqlClient;
using Bogus;

public class TestDataCreate
{
    static void Main(string[] args)
    {     
        string connectionString = "Server=localhost;Database=MyDatabase;Trusted_Connection=true;";

        int rowCount = 10000;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO TestTable (Column1, Column2, Column3, Column4, Column5, Column6) " +
                                      "VALUES (@Column1,@Column2 , @Column3,  @Column4,  @Column5,  @Column6)";
            SqlParameter parameterColumn1 = command.Parameters.Add("@Column1", System.Data.SqlDbType.NVarChar);
            SqlParameter parameterColumn2 = command.Parameters.Add("@Column2", System.Data.SqlDbType.NVarChar);
            SqlParameter parameterColumn3 = command.Parameters.Add("@Column3", System.Data.SqlDbType.Int);
            SqlParameter parameterColumn4 = command.Parameters.Add("@Column4", System.Data.SqlDbType.DateTime);
            SqlParameter parameterColumn5 = command.Parameters.Add("@Column5", System.Data.SqlDbType.Decimal);
            SqlParameter parameterColumn6 = command.Parameters.Add("@Column6", System.Data.SqlDbType.Bit);


            Bogus.Faker faker = new Bogus.Faker();
            
            for (int i = 1; i <= rowCount; i++)
            {
                var person = faker.Person;
                parameterColumn1.Value = person.FirstName;
                parameterColumn2.Value = person.LastName;
                parameterColumn3.Value = faker.Random.Int(1, 100);
                parameterColumn4.Value = faker.Date.Past();                
                parameterColumn5.Value = faker.Random.Decimal(1, 1000);
                parameterColumn6.Value = faker.Random.Bool();

                command.ExecuteNonQuery();
            }
        }
            Console.WriteLine("テストデータの生成終了");
            Console.ReadLine();
        
    }
}
