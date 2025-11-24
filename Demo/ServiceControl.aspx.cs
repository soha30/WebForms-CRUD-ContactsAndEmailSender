using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using soha_f6269.App_Code;


namespace soha_f6269.Demo
{
    public partial class ServiceControl : System.Web.UI.Page
    {
      

        /// <summary>
        /// Handles the Page Load event to initialize the page. 
        /// If the page is being loaded for the first time (not a postback), 
        /// it populates the country dropdown list with data from the database.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the page is being loaded for the first time and not as a result of a postback
            if (!Page.IsPostBack)
            {
                // Populate the country dropdown list with data from the database
                PopulateCountryDropdown();
            }
        }
        /// <summary>
        /// Handles the submit button click event to insert new contact details into the database
        /// and refreshes the contact grid to display the updated data.
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {    // Create a dictionary to hold the contact details
            // Use the var keyword to declare the contactDetails(myPara) dictionary
            // The compiler infers the type as Dictionary<string, object> based on the assigned value
            var contactDetails = new Dictionary<string, object>
            //myPara
            {
              { "@fName", txtfName.Text},       // Add first name from textbox
              { "@lName", txtlName.Text},       // Add last name from textbox 
              { "@cell", txtCell.Text},         // Add cell number from textbox
              { "@email", txtEmail.Text},       // Add email from textbox
              { "@countryId", ddlCountry.SelectedValue} // Add selected country ID from dropdown list
            };
              // Define the SQL query to insert the contact details into the database
              string query = @"INSERT INTO contact (fName, lName, cell, email, countryId) 
                 VALUES (@fName, @lName, @cell, @email, @countryId)";

            // Execute the query and get the number of affected rows
            int result = ExecuteNonQuery(query, contactDetails);
            //Use ternary operator to set the label text based on the success or failure of the operatio
             lblOutput.Text = result >= 1 ? "Operation successful!" : "Operation failed!";

            // Refresh or repopulate the contact grid to reflect the new data
            PopulateContactGrid();
        }
        /// <summary>
        /// Handles the show contact info button click event to populate the contact grid
        /// with the latest contact details from the database.
        /// </summary>
        protected void btnShowContactInfo_Click(object sender, EventArgs e)
        {
           
                PopulateContactGrid();
           
        }
        /// <summary>
        /// Handles the update button click event to update existing contact details
        /// in the database and refreshes the contact grid to display the updated data.
        /// </summary>
        /// <summary>
        /// Handles the update button click event to update existing contact details
        /// in the database and refreshes the contact grid to display the updated data.
        /// </summary>
        protected void btnUbdate_Click(object sender, EventArgs e)
        {
            // Create a dictionary to hold the updated contact details
            // The compiler infers the type as Dictionary<string, object> based on the assigned values
            var contactDetails = new Dictionary<string, object>
    {
        { "@fName", txtfName.Text },       // Add first name from textbox
        { "@lName", txtlName.Text },       // Add last name from textbox 
        { "@cell", txtCell.Text },         // Add cell number from textbox
        { "@email", txtEmail.Text },       // Add email from textbox
        { "@countryId", ddlCountry.SelectedValue }, // Add selected country ID from dropdown list
        { "@contactId", txtContactId.Text } // Add contact ID from textbox
    };

            // Define the SQL query to update the contact details in the database
            string sql = @"UPDATE contact
                     SET fName = @fName, lName = @lName, cell = @cell, email = @email, countryId = @countryId
                     WHERE contactId = @contactId";

            // Execute the query and get the number of affected rows
            int result = ExecuteNonQuery(sql, contactDetails);

            // Use ternary operator to set the label text based on the success or failure of the operation
            lblOutput.Text = result >= 1 ? "Operation successful!" : "Operation failed!";

            // Refresh or repopulate the contact grid to reflect the updated data
            PopulateContactGrid();
        }
        /// <summary>
        /// Handles the delete button click event to delete a contact from the database
        /// and refreshes the contact grid to display the updated data.
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Try to parse the contact ID from the textbox
            if (int.TryParse(txtContactId.Text, out int contactId))
            {
                // Create a dictionary to hold the parameter for the SQL query
                var parameters = new Dictionary<string, object> { { "@contactId", contactId } };

                // Define the SQL query to delete the contact from the database
                string query = "DELETE FROM contact WHERE contactId = @contactId";

                // Execute the query and get the number of affected rows
                int result = ExecuteNonQuery(query, parameters);

                // Use ternary operator to set the label text based on the success or failure of the operation
                lblOutput.Text = result >= 1 ? "Operation successful!" : "Operation failed!";

                // Clear the contact ID textbox
                txtContactId.Text = string.Empty;

                // Refresh or repopulate the contact grid to reflect the updated data
                PopulateContactGrid();
            }
            else
            {
                // Set the label text to indicate an invalid contact ID
                lblOutput.Text = "Invalid contact ID.";
            }
        }
       /// <summary>
        /// Executes a non-query SQL statement (INSERT, UPDATE, DELETE) with the provided parameters.
        /// </summary>
        /// <param name="sql">The SQL query string to be executed.</param>
        /// <param name="parameters">A dictionary containing the parameters for the SQL query.</param>
        /// <returns>The number of rows affected by the SQL query.</returns>
        private int ExecuteNonQuery(string sql, Dictionary<string, object> parameters)
        {
            // Create an instance of the CRUD class to perform the database operation
            CRUD crud = new CRUD();

            // Execute the SQL query using the CRUD class's InsertUpdateDelete method and return the result
            return crud.InsertUpdateDelete(sql, parameters);
        }
        /// <summary>
        /// Executes a SQL query that returns a data reader.
        /// <summary>
        /// Executes a SQL query that returns a data reader.
        /// </summary>
        /// <param name="query">The SQL query to be executed.</param>
        /// <returns>A SqlDataReader object containing the results of the query.</returns>
        private SqlDataReader ExecuteReader(string query)
        {
            // Create an instance of the CRUD class to perform the database operation
            CRUD crud = new CRUD();

            // Execute the SQL query using the CRUD class's getDrPassSql method and return the SqlDataReader
            return crud.getDrPassSql(query);
        }
       /// <summary>
        /// Populates the contact grid by retrieving contact details from the database
        /// and binding the results to the GridView control.
        /// </summary>
        private void PopulateContactGrid()
        {
            // Define the SQL query to select contact details along with the country name
            // The query joins the contact table with the country table on the countryId field
            string query = @"SELECT contactId, fName, lName, cell, email, country
                     FROM contact c
                     INNER JOIN country co ON c.countryId = co.countryId";

            // Set the data source of the GridView to the result of the ExecuteReader method
            // ExecuteReader method should return a data set or data table with the query results
            gvContact.DataSource = ExecuteReader(query);

            // Bind the data to the GridView to display the results
            gvContact.DataBind();
        }
        /// <summary>
        /// Populates the country dropdown list with country data from the database.
        /// </summary>
        private void PopulateCountryDropdown()
        {
            // Define the SQL query to select country IDs and country names from the country table
            string query = @"SELECT countryId, country FROM country";

            // Set the data source of the dropdown list to the result of the ExecuteReader method
            ddlCountry.DataSource = ExecuteReader(query);

            // Specify the field to display in the dropdown list
            ddlCountry.DataTextField = "country";

            // Specify the field to use as the value for each item in the dropdown list
            ddlCountry.DataValueField = "countryId";

            // Bind the data to the dropdown list to display the items
            ddlCountry.DataBind();
        }
        /// </summary>Failed attempt
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">A dictionary containing parameters for the SQL query.</param>
        /// <returns>The number of rows affected by the command.</returns>
        //private int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        //{
        //    // Initialize CRUD object to handle database operations
        //    CRUD crud = null;

        //    try
        //    {
        //        // Create a new instance of CRUD to perform the database operation
        //        crud = new CRUD();

        //        // Execute the query with the provided parameters and return the number of affected rows
        //        return crud.InsertUpdateDelete(query, parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions that occur during execution
        //        // Output the error message to the label for user feedback
        //        lblOutput.Text = $"Error: {ex.Message}";

        //        // Return 0 to indicate that no rows were affected due to the error
        //        return 0;
        //    }
        //    finally
        //    {
        //        // Ensure that the CRUD object is properly disposed of to release any resources
        //        if (crud != null)
        //        {
        //            // Release the resources held by the object
        //            crud.Dispose();      //Garbage Collector
        //        }
        //    }
        //}
        /// <summary>  Failed attempt
        /// Executes a SQL query that returns a data reader.
        /// Added error handling and ensured resources are properly managed.
        /// </summary>
        /// <param name="query">The SQL query to be executed.</param>
        /// <returns>A SqlDataReader object containing the results of the query.</returns>
        //private SqlDataReader ExecuteReader(string query)
        //{
        //    CRUD crud = null;
        //    try
        //    {
        //        crud = new CRUD();
        //        return crud.getDrPassSql(query);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handling exceptions and providing useful error messages.
        //        lblOutput.Text = $"Error: {ex.Message}";
        //        return null;
        //    }
        //    finally
        //    {
        //        //Ensuring the resource is disposed of properly.
        //        if (crud != null)
        //        {
        //            string value = crud;
        //            crud.Dispose();
        //        }
        //    }
        //}
        /// <summary>  Failed attempt
        /// Executes a SQL query that returns a data reader.
        /// Added error handling and ensured resources are properly managed.
        /// </summary>
        /// <param name="query">The SQL query to be executed.</param>
        /// <returns>A DataTable object containing the results of the query.</returns>
        /// <summary>
        /// Executes a SQL query that returns a data reader.
        /// Added error handling and ensured resources are properly managed.
        /// </summary>
        /// <param name="query">The SQL query to be executed.</param>
        /// <returns>A DataTable object containing the results of the query.</returns>
        //private DataTable ExecuteReader(string query)
        //{
        //    // Create a DataTable object
        //    DataTable dataTable = new DataTable();
        //    SqlDataReader reader = null;
        //    CRUD crud = null;
        //    try
        //    {
        //        // Create a CRUD object and get SqlDataReader
        //        crud = new CRUD();
        //        reader = crud.getDrPassSql(query);

        //        // Check if the reader has any rows
        //        if (reader.HasRows)
        //        {
        //            // Log the number of columns
        //            int fieldCount = reader.FieldCount;
        //            lblOutput.Text = $"Number of columns: {fieldCount}";

        //            // Iterate through the reader to count rows
        //            int rowCount = 0;
        //            while (reader.Read())
        //            {
        //                rowCount++;
        //            }

        //            // Reset the reader to the beginning
        //            reader.Close();
        //            reader = crud.getDrPassSql(query);

        //            lblOutput.Text += $", Number of rows: {rowCount}";

        //            // Load data into DataTable
        //            dataTable.Load(reader);
        //        }
        //        else
        //        {
        //            // Log if there are no rows
        //            lblOutput.Text = "No rows found.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions and provide useful error messages
        //        lblOutput.Text = $"Error: {ex.Message}";
        //    }
        //    finally
        //    {
        //        // Ensure the SqlDataReader is closed if it's not null
        //        if (reader != null)
        //        {
        //            reader.Close();
        //        }
        //        // Dispose the CRUD object
        //        if (crud != null)
        //        {
        //            crud.Dispose();
        //        }
        //    }
        //    // Return the DataTable
        //    return dataTable;
        //}
        /// <summary>   Failed attempt
        /// Executes a SQL query that returns a data reader.
        /// Added error handling and ensured resources are properly managed.
        /// </summary>
        /// <param name="query">The SQL query to be executed.</param>
        /// <returns>A DataTable object containing the results of the query.</returns>
        //private DataTable ExecuteReader(string query)
        //{
        //    // Create a DataTable object
        //    DataTable dataTable = new DataTable();
        //    SqlDataReader reader = null;
        //    CRUD crud = null;
        //    try
        //    {
        //        // Create a CRUD object and get SqlDataReader
        //        crud = new CRUD();
        //        reader = crud.getDrPassSql(query);

        //        // Check if the reader has any rows
        //        if (reader.HasRows)
        //        {
        //            // Log the number of columns
        //            int fieldCount = reader.FieldCount;
        //            lblOutput.Text = $"Number of columns: {fieldCount}";

        //            // Iterate through the reader to count rows
        //            int rowCount = 0;
        //            while (reader.Read())
        //            {
        //                rowCount++;
        //            }

        //            // Reset the reader to the beginning
        //            reader.Close();
        //            reader = crud.getDrPassSql(query);

        //            lblOutput.Text += $", Number of rows: {rowCount}";

        //            // Load data into DataTable
        //            dataTable.Load(reader);
        //        }
        //        else
        //        {
        //            // Log if there are no rows
        //            lblOutput.Text = "No rows found.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions and provide useful error messages
        //        lblOutput.Text = $"Error: {ex.Message}";
        //    }
        //    finally
        //    {
        //        // Ensure the SqlDataReader is closed if it's not null
        //        if (reader != null)
        //        {
        //            reader.Close();
        //        }
        //        // Dispose the CRUD object
        //        if (crud != null)
        //        {
        //            crud.Dispose();
        //        }
        //    }
        //    // Return the DataTable
        //    return dataTable;
        //}

    }

}

