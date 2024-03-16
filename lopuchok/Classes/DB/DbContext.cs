using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lopuchok.Classes.DB
{
    public class DbContext
    {
        const string connectionString = "Host=localhost;Database=LopushokDb;Username=postgres;Password=0211";

        public NpgsqlConnection connection = new NpgsqlConnection(connectionString);

        public List<Product> GetAllProduct()
        {
            List<Product> products = new List<Product>();

            string sqlProduct = "SELECT * FROM \"Product\"";

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlProduct, connection))
                {
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ID = Convert.ToInt32(reader.GetValue(0));
                        product.Title = reader.GetValue(1).ToString();
                        int IdProductTypeID = Convert.ToInt32(reader.GetValue(2));
                        product.ArticleNumber = reader.GetValue(3).ToString();
                        product.Description = reader.GetValue(4).ToString();
                        product.Image = reader.GetValue(5).ToString();
                        product.ProductionPersonCount = reader.GetValue(6) != DBNull.Value ? Convert.ToInt32(reader.GetValue(6)) : 0;
                        product.ProductionWorkshopNumber = reader.GetValue(7) != DBNull.Value ? Convert.ToInt32(reader.GetValue(7)) : 0;
                        product.MinCostForAgent = reader.GetValue(8) != DBNull.Value ? Convert.ToDouble(reader.GetValue(8)) : 0.0;
                        int[] IndexMaterials;
                        using (NpgsqlConnection innerConnection = new NpgsqlConnection(connectionString))
                        {
                            IndexMaterials = GetMaterialIdForIDProduct(product.ID, innerConnection);
                        }
                        ProductType productType;
                        using (NpgsqlConnection innerConnection2 = new NpgsqlConnection(connectionString))
                        {
                            productType = GetProductTypeForId(IdProductTypeID, innerConnection2);
                        }

                        product.ProductType = productType;
                        List<Material> materials;
                        using (NpgsqlConnection innerConnection3 = new NpgsqlConnection(connectionString))
                        {
                            materials = GetMaterialsForId(IndexMaterials, innerConnection3);
                        }
                        product.Materials = materials;
                        products.Add(product);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return products;
        }

        public ProductType GetProductTypeForId(int ProductTypeId, NpgsqlConnection connection)
        {
            ProductType productType = new ProductType();
            try
            {
                connection.Open();
                string sql = $"SELECT * FROM \"ProductType\" WHERE \"ID\" = @ID";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {

                    command.Parameters.AddWithValue("@ID", ProductTypeId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        productType.ID = Convert.ToInt32(reader.GetValue(0));
                        productType.Title = reader.GetValue(1).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return productType;
        }

        public int[] GetMaterialIdForIDProduct(int productId, NpgsqlConnection connection)
        {
            List<int> index = new List<int>();
            string sql = "SELECT \"MaterialID\" FROM \"ProductMaterial\" WHERE \"ProductID\" = @ProductID";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@ProductId", productId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int MaterialIndex = Convert.ToInt32(reader.GetValue(0));
                        index.Add(MaterialIndex);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return index.ToArray();
        }

        public List<Material> GetMaterialsForId(int[] IdMaterials, NpgsqlConnection connection)
        {
            List<Material> materials = new List<Material>();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM \"Material\" WHERE \"ID\" = ANY(@IdMaterials)";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdMaterials", IdMaterials);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Material material = new Material();
                        material.ID = Convert.ToInt32(reader["ID"]);
                        material.Title = reader["Title"].ToString();
                        materials.Add(material);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return materials;
        }

        public List<Product> GetProductSearch(string str, int index)
        {
            List<Product> products = new List<Product>();
            try
            {
                string sql = $"SELECT * FROM \"Product\"";
                if (str.Length != 0)
                {
                    sql += $"WHERE \"Title\" ilike '%{str}%'";
                }
                switch (index)
                {
                    case 0:
                        sql += " ORDER BY \"Title\" ASC";
                        break;
                    case 1:
                        sql += " ORDER BY \"Title\" DESC";
                        break;
                    case 2:
                        sql += " ORDER BY \"ProductionWorkshopNumber\" ASC";
                        break;
                    case 3:
                        sql += " ORDER BY \"ProductionWorkshopNumber\" DESC";
                        break;
                    case 4:
                        sql += " ORDER BY \"ProductionPersonCount\" ASC";
                        break;
                    case 5:
                        sql += " ORDER BY \"ProductionPersonCount\" DESC";
                        break;
                }
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.ID = Convert.ToInt32(reader.GetValue(0));
                    product.Title = reader.GetValue(1).ToString();
                    int IdProductTypeID = Convert.ToInt32(reader.GetValue(2));
                    product.ArticleNumber = reader.GetValue(3).ToString();
                    product.Description = reader.GetValue(4).ToString();
                    product.Image = reader.GetValue(5).ToString();
                    product.ProductionPersonCount = reader.GetValue(6) != DBNull.Value ? Convert.ToInt32(reader.GetValue(6)) : 0;
                    product.ProductionWorkshopNumber = reader.GetValue(7) != DBNull.Value ? Convert.ToInt32(reader.GetValue(7)) : 0;
                    product.MinCostForAgent = reader.GetValue(8) != DBNull.Value ? Convert.ToDouble(reader.GetValue(8)) : 0.0;
                    int[] IndexMaterials;
                    using (NpgsqlConnection innerConnection = new NpgsqlConnection(connectionString))
                    {
                        IndexMaterials = GetMaterialIdForIDProduct(product.ID, innerConnection);
                    }
                    ProductType productType;
                    using (NpgsqlConnection innerConnection2 = new NpgsqlConnection(connectionString))
                    {
                        productType = GetProductTypeForId(IdProductTypeID, innerConnection2);
                    }

                    product.ProductType = productType;
                    List<Material> materials;
                    using (NpgsqlConnection innerConnection3 = new NpgsqlConnection(connectionString))
                    {
                        materials = GetMaterialsForId(IndexMaterials, innerConnection3);
                    }
                    product.Materials = materials;
                    products.Add(product);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return products;
        }

        public List<ProductType> GetAllProductType()
        {
            List<ProductType> ListproductType = new List<ProductType>();
            try
            {
                connection.Open();
                string sql = $"SELECT * FROM \"ProductType\"";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductType productType = new ProductType();
                        productType.ID = Convert.ToInt32(reader.GetValue(0));
                        productType.Title = reader.GetValue(1).ToString();
                        ListproductType.Add(productType);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return ListproductType;
        }

        public List<Material> GetAllMaterial()
        {
            List<Material> materials = new List<Material>();
            try
            {
                connection.Open();
                string sql = $"SELECT * FROM \"Material\"";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Material material = new Material();
                        material.ID = Convert.ToInt32(reader.GetValue(0));
                        material.Title = reader.GetValue(1).ToString();
                        materials.Add(material);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return materials;
        }

        public void Save(Product product)
        {
            try
            {
                connection.Open();
                if (product.ID == 0)
                {
                    string Sql = "INSERT INTO \"Product\"(\"Title\",\"ArticleNumber\",\"Image\",\"Description\",\"ProductTypeID\") VALUES (@Title,@ArticleNumber,@Image,@Description,@ProductTypeID)";
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand(Sql, connection);
                    command.Parameters.AddWithValue("@Title", product.Title);
                    command.Parameters.AddWithValue("@ArticleNumber", product.ArticleNumber);
                    command.Parameters.AddWithValue("@Image", product.Image);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@ProductTypeID", product.ProductType.ID);
                    command.ExecuteNonQuery();
                   //ДОПИСАТЬ ЛОГИКУ ДОБАВЛЕНИЯ МАТЕРИАЛОВ
                    MessageBox.Show("Данные добавлены ");
                   
                }
                else
                {
                    string query = "UPDATE \"Product\" SET \"Title\" = @Title, \"ArticleNumber\" = @ArticleNumber, \"Image\" = @Image,\"Description\" = @Description, \"ProductTypeID\" = @ProductTypeID WHERE \"ID\" = @ProductId";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", product.Title);
                        command.Parameters.AddWithValue("@ArticleNumber", product.ArticleNumber);
                        command.Parameters.AddWithValue("@Image", product.Image);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@ProductTypeID", product.ProductType.ID);
                        command.Parameters.AddWithValue("@ProductId", product.ID);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Данные обновлены ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void SetProductMaterial(Product product, NpgsqlConnection connection)
        {
            
            try
            {
                connection.Open();
                string query = "DELETE FROM \"ProductMaterial\" WHERE  \"ProductID\" =@ProductID";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", product.ID);
                    command.ExecuteNonQuery();
                }
                string query2 = "INSERT INTO \"ProductMaterial\"(\"ProductID\",\"MaterialID\") VALUES (@ProductID,@MaterialID)";

                for (int i = 0; i < product.Materials.Count; i++)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", product.ID);
                        command.Parameters.AddWithValue("@MaterialID", product.Materials[i].ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
