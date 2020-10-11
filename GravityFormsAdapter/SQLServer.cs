using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace GravityFormsAdapter
{
    class SQLServer
    {
        internal static void SaveForms(GravityDataStructures.GravityForm form)
        {
            using (var db = new GravityFormsSQLEntities())
            {
                db.Database.ExecuteSqlCommand("DELETE i FROM tblGravityFieldInputs i INNER JOIN tblGravityFields e ON e.id = i.field_id INNER JOIN tblGravityForms f ON f.id = e.form_id WHERE f.id = " + form.id);
                db.Database.ExecuteSqlCommand("DELETE e FROM tblGravityFields e INNER JOIN tblGravityForms f ON f.id = e.form_id WHERE f.id = " + form.id);
                db.Database.ExecuteSqlCommand("DELETE f FROM tblGravityForms f WHERE f.id = " + form.id);
            }

            using (var db = new GravityFormsSQLEntities())
            {
                var formRec = db.tblGravityForms.Create();

                formRec.id = form.id;
                formRec.title = form.title;
                formRec.description = form.description;

                foreach (var field in form.fields)
                {
                    var fieldRec = db.tblGravityFields.Create();

                    fieldRec.form_id = form.id;
                    fieldRec.label = field.label;
                    fieldRec.type = field.type;
                    fieldRec.description = field.description;


                    db.tblGravityFields.Add(fieldRec);

                    if (field.inputs != null)
                    {
                        db.SaveChanges(); // Do this to get the field ID for the next insert (this would be neater using foreign keys)

                        foreach (var input in field.inputs)
                        {
                            var inputRec = db.tblGravityFieldInputs.Create();

                            inputRec.field_id = fieldRec.id;
                            inputRec.label = input.label;
                            inputRec.name = input.name;

                            db.tblGravityFieldInputs.Add(inputRec);
                        }
                    }
                }

                db.tblGravityForms.Add(formRec);

                db.SaveChanges();
            }

        }
        internal static void SaveEntries(GravityDataStructures.GravityEntry entry)
        {
            using (var db = new GravityFormsSQLEntities())
            {
                db.Database.ExecuteSqlCommand("DELETE v FROM tblGravityEntryValues v INNER JOIN tblGravityEntries e ON e.id = v.entry_id WHERE e.id = " + entry.id);
                db.Database.ExecuteSqlCommand("DELETE e FROM tblGravityEntries e WHERE e.id = " + entry.id);
            }

            using (var db = new GravityFormsSQLEntities())
            {
                var entryRec = db.tblGravityEntries.Create();

                entryRec.id = entry.id;
                if (Int32.TryParse(entry.form_id, out int form_id))
                {
                    // Not sure why this is specified as a string in the API docs
                    entryRec.form_id = Int32.Parse(entry.form_id);
                }
                else
                {
                    throw new Exception("Could not parse the form_id for entry id = " + entry.id.ToString());
                }
                entryRec.ip = entry.ip;
                entryRec.date_created = entry.date_created;
                entryRec.created_by = entry.created_by;
                entryRec.currency = entry.currency;
                entryRec.is_fulfilled = entry.is_fulfilled;
                entryRec.is_read = entry.is_fulfilled;
                entryRec.is_starred = entry.is_starred;
                entryRec.payment_amount = entry.payment_amount;
                entryRec.payment_date = entry.payment_date;
                entryRec.payment_status = entry.payment_status;
                entryRec.post_id = entry.post_id;
                entryRec.source_url = entry.source_url;
                entryRec.status = entry.status;
                entryRec.transaction_id = entry.transaction_id;
                entryRec.transaction_type = entry.transaction_type;
                entryRec.user_agent = entry.user_agent;

                foreach (var field in entry.formValues)
                {
                    var valueRec = db.tblGravityEntryValues.Create();

                    valueRec.entry_id = entry.id;
                    valueRec.entryKey = field.Key.ToString();
                    valueRec.entrySubKey = field.Value.Item2;
                    valueRec.entryValue = field.Value.Item3;

                    db.tblGravityEntryValues.Add(valueRec);
                }

                db.tblGravityEntries.Add(entryRec);

                db.SaveChanges();
            }
        }
    }
}
