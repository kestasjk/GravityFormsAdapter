using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GravityFormsAdapter
{
    public class GravityDataStructures
    {
        public class APIV1Envelope<T> where T: class
        {
            public int status { get; set; }
            public Dictionary<string, T> response { get; set; }
        }
        [DataContract]
        public class GravityForm
        {// http://jigsaw.kuliukas.com/wp-json/gf/v2/forms
         // http://jigsaw.kuliukas.com/wp-json/gf/v2/forms/1

            //{"1":{"id":"1","title":"Test Gravity Form","entries":"1"}}

            /*
             * {"title":"Test Gravity Form","description":"Chris' test gravity form.","labelPlacement":"top_label","descriptionPlacement":"below","button":{"type":"text","text":"Submit","imageUrl":""},"fields":[{"type":"name","id":2,"label":"Your Name","adminLabel":"","isRequired":true,"size":"medium","errorMessage":"","visibility":"visible","nameFormat":"advanced","inputs":[{"id":"2.2","label":"Prefix","name":"","choices":[{"text":"Dr.","value":"Dr."},{"text":"Miss","value":"Miss"},{"text":"Mr.","value":"Mr."},{"text":"Mrs.","value":"Mrs."},{"text":"Ms.","value":"Ms."},{"text":"Prof.","value":"Prof."},{"text":"Rev.","value":"Rev."}],"isHidden":true,"inputType":"radio"},{"id":"2.3","label":"First","name":""},{"id":"2.4","label":"Middle","name":"","isHidden":true},{"id":"2.6","label":"Last","name":""},{"id":"2.8","label":"Suffix","name":"","isHidden":true}],"formId":1,"description":"Please enter your name.","allowsPrepopulate":false,"inputMask":false,"inputMaskValue":"","inputMaskIsCustom":false,"maxLength":"","inputType":"","labelPlacement":"","descriptionPlacement":"","subLabelPlacement":"","placeholder":"","cssClass":"","inputName":"","noDuplicates":false,"defaultValue":"","choices":"","conditionalLogic":"","productField":"","multipleFiles":false,"maxFiles":"","calculationFormula":"","calculationRounding":"","enableCalculation":"","disableQuantity":false,"displayAllCategories":false,"useRichTextEditor":false,"checkboxLabel":"","pageNumber":1,"fields":"","displayOnly":"","personalDataExport":true,"personalDataErase":true},{"type":"section","id":10,"label":"Details","adminLabel":"","isRequired":false,"size":"medium","errorMessage":"","visibility":"visible","inputs":null,"displayOnly":true,"formId":1,"description":"Please enter the details of your enquiry below","allowsPrepopulate":false,"inputMask":false,"inputMaskValue":"","inputMaskIsCustom":false,"maxLength":"","inputType":"","labelPlacement":"","descriptionPlacement":"","subLabelPlacement":"","placeholder":"","cssClass":"","inputName":"","noDuplicates":false,"defaultValue":"","choices":"","conditionalLogic":"","productField":"","multipleFiles":false,"maxFiles":"","calculationFormula":"","calculationRounding":"","enableCalculation":"","disableQuantity":false,"displayAllCategories":false,"useRichTextEditor":false,"checkboxLabel":"","pageNumber":1,"fields":"","personalDataExport":false,"personalDataErase":false},{"type":"checkbox","id":8,"label":"How did you hear about us?","adminLabel":"","isRequired":false,"size":"medium","errorMessage":"","visibility":"visible","choices":[{"text":"Search engine","value":"Search engine","isSelected":false,"price":""},{"text":"Recommendation","value":"Recommendation","isSelected":false,"price":""},{"text":"Magazine","value":"Magazine","isSelected":false,"price":""}],"inputs":[{"id":"8.1","label":"Search engine","name":""},{"id":"8.2","label":"Recommendation","name":""},{"id":"8.3","label":"Magazine","name":""}],"formId":1,"description":"","allowsPrepopulate":false,"inputMask":false,"inputMaskValue":"","inputMaskIsCustom":false,"maxLength":"","inputType":"","labelPlacement":"","descriptionPlacement":"","subLabelPlacement":"","placeholder":"","cssClass":"","inputName":"","noDuplicates":false,"defaultValue":"","conditionalLogic":"","productField":"","enableSelectAll":"","enablePrice":"","multipleFiles":false,"maxFiles":"","calculationFormula":"","calculationRounding":"","enableCalculation":"","disableQuantity":false,"displayAllCategories":false,"useRichTextEditor":false,"pageNumber":1,"fields":"","displayOnly":"","personalDataExport":true,"personalDataErase":true},{"type":"radio","id":9,"label":"How old are you?","adminLabel":"","isRequired":true,"size":"medium","errorMessage":"","visibility":"visible","inputs":null,"choices":[{"text":"13-20","value":"13-20","isSelected":false,"price":""},{"text":"20-30","value":"20-30","isSelected":false,"price":""},{"text":"30-40","value":"30-40","isSelected":false,"price":""},{"text":"40-50","value":"40-50","isSelected":false,"price":""},{"text":"50-60","value":"50-60","isSelected":false,"price":""},{"text":"60-70","value":"60-70","isSelected":false,"price":""},{"text":"70+","value":"70+","isSelected":false,"price":""}],"formId":1,"description":"","allowsPrepopulate":false,"inputMask":false,"inputMaskValue":"","inputMaskIsCustom":false,"maxLength":"","inputType":"","labelPlacement":"","descriptionPlacement":"","subLabelPlacement":"","placeholder":"","cssClass":"","inputName":"","noDuplicates":false,"defaultValue":"","conditionalLogic":"","productField":"","enableOtherChoice":"","enablePrice":"","multipleFiles":false,"maxFiles":"","calculationFormula":"","calculationRounding":"","enableCalculation":"","disableQuantity":false,"displayAllCategories":false,"useRichTextEditor":false,"pageNumber":1,"fields":"","displayOnly":"","personalDataExport":true,"personalDataErase":true},{"type":"select","id":12,"label":"Are you affiliated?","adminLabel":"","isRequired":true,"size":"medium","errorMessage":"","visibility":"visible","inputs":null,"choices":[{"text":"No affiliation","value":"No affiliation","isSelected":true,"price":""},{"text":"Member","value":"Member","isSelected":false,"price":""},{"text":"Employee","value":"Employee","isSelected":false,"price":""}],"formId":1,"description":"","allowsPrepopulate":false,"inputMask":false,"inputMaskValue":"","inputMaskIsCustom":false,"maxLength":"","inputType":"","labelPlacement":"","descriptionPlacement":"","subLabelPlacement":"","placeholder":"","cssClass":"","inputName":"","noDuplicates":false,"defaultValue":"","conditionalLogic":"","productField":"","enablePrice":"","multipleFiles":false,"maxFiles":"","calculationFormula":"","calculationRounding":"","enableCalculation":"","disableQuantity":false,"displayAllCategories":false,"useRichTextEditor":false,"pageNumber":1,"fields":"","displayOnly":"","personalDataExport":true,"personalDataErase":true},{"type":"multiselect","id":13,"label":"What type of inquiry is this?","adminLabel":"","isRequired":true,"size":"medium","errorMessage":"","visibility":"visible","storageType":"json","inputs":null,"choices":[{"text":"Finding a parent","value":"Finding a parent","isSelected":false,"price":""},{"text":"Finding a child","value":"Finding a child","isSelected":false,"price":""},{"text":"Hiding from a parent \/ child","value":"Hiding from a parent \/ child","isSelected":false,"price":""},{"text":"Other","value":"Other","isSelected":false,"price":""}],"formId":1,"description":"","allowsPrepopulate":false,"inputMask":false,"inputMaskValue":"","inputMaskIsCustom":false,"maxLength":"","inputType":"","labelPlacement":"","descriptionPlacement":"","subLabelPlacement":"","placeholder":"","cssClass":"","inputName":"","noDuplicates":false,"defaultValue":"","conditionalLogic":"","enableEnhancedUI":false,"productField":"","multiSelectSize":"","enablePrice":"","multipleFiles":false,"maxFiles":"","calculationFormula":"","calculationRounding":"","enableCalculation":"","disableQuantity":false,"displayAllCategories":false,"useRichTextEditor":false,"enableChoiceValue":false,"pageNumber":1,"fields":"","displayOnly":"","personalDataExport":true,"personalDataErase":true},{"type":"textarea","id":14,"label":"Please describe your enquiry.","adminLabel":"","isRequired":true,"size":"medium","errorMessage":"","visibility":"visible","inputs":null,"formId":1,"description":"Please omit any confidential information.","allowsPrepopulate":false,"inputMask":false,"inputMaskValue":"","inputMaskIsCustom":false,"maxLength":"","inputType":"","labelPlacement":"","descriptionPlacement":"","subLabelPlacement":"","placeholder":"","cssClass":"","inputName":"","noDuplicates":false,"defaultValue":"","choices":"","conditionalLogic":"","productField":"","form_id":"","useRichTextEditor":false,"multipleFiles":false,"maxFiles":"","calculationFormula":"","calculationRounding":"","enableCalculation":"","disableQuantity":false,"displayAllCategories":false,"checkboxLabel":"","pageNumber":1,"fields":"","displayOnly":"","personalDataExport":true,"personalDataErase":true},{"type":"section","id":11,"label":"Contact details","adminLabel":"","isRequired":false,"size":"medium","errorMessage":"","visibility":"visible","inputs":null,"displayOnly":true,"formId":1,"description":"How should we get in contact with you","allowsPrepopulate":false,"inputMask":false,"inputMaskValue":"","inputMaskIsCustom":false,"maxLength":"","inputType":"","labelPlacement":"","descriptionPlacement":"","subLabelPlacement":"","placeholder":"","cssClass":"","inputName":"","noDuplicates":false,"defaultValue":"","choices":"","conditionalLogic":"","productField":"","multipleFiles":false,"maxFiles":"","calculationFormula":"","calculationRounding":"","enableCalculation":"","disableQuantity":false,"displayAllCategories":false,"useRichTextEditor":false,"checkboxLabel":"","pageNumber":1,"fields":"","personalDataExport":false,"personalDataErase":false},{"type":"phone","id":6,"label":"Phone","adminLabel":"","isRequired":false,"size":"medium","errorMessage":"","visibility":"visible","inputs":null,"phoneFormat":"standard","formId":1,"description":"","allowsPrepopulate":false,"inputMask":false,"inputMaskValue":"","inputMaskIsCustom":"","maxLength":"","inputType":"","labelPlacement":"","descriptionPlacement":"","subLabelPlacement":"","placeholder":"","cssClass":"","inputName":"","noDuplicates":false,"defaultValue":"","choices":"","conditionalLogic":"","productField":"","pageNumber":1,"fields":"","displayOnly":"","personalDataExport":true,"personalDataErase":true},{"type":"email","id":4,"label":"Email","adminLabel":"","isRequired":false,"size":"medium","errorMessage":"","visibility":"visible","inputs":null,"formId":1,"description":"","allowsPrepopulate":false,"inputMask":false,"inputMaskValue":"","inputMaskIsCustom":"","maxLength":"","inputType":"","labelPlacement":"","descriptionPlacement":"","subLabelPlacement":"","placeholder":"","cssClass":"","inputName":"","noDuplicates":false,"defaultValue":"","choices":"","conditionalLogic":"","productField":"","emailConfirmEnabled":"","pageNumber":1,"fields":"","displayOnly":"","personalDataExport":true,"personalDataErase":true},{"type":"address","id":7,"label":"Address","adminLabel":"","isRequired":false,"size":"medium","errorMessage":"","visibility":"visible","addressType":"international","inputs":[{"id":"7.1","label":"Street Address","name":""},{"id":"7.2","label":"Address Line 2","name":""},{"id":"7.3","label":"City","name":""},{"id":"7.4","label":"State \/ Province","name":""},{"id":"7.5","label":"ZIP \/ Postal Code","name":""},{"id":"7.6","label":"Country","name":""}],"formId":1,"description":"","allowsPrepopulate":false,"inputMask":false,"inputMaskValue":"","inputMaskIsCustom":"","maxLength":"","inputType":"","labelPlacement":"","descriptionPlacement":"","subLabelPlacement":"","placeholder":"","cssClass":"","inputName":"","noDuplicates":false,"defaultValue":"","choices":"","conditionalLogic":"","defaultCountry":"","defaultProvince":"","copyValuesOptionLabel":"","productField":"","hideCountry":"","defaultState":"","hideState":"","hideAddress2":"","enableCopyValuesOption":"","copyValuesOptionDefault":"","pageNumber":1,"fields":"","displayOnly":"","personalDataExport":true,"personalDataErase":true}],"version":"2.4.20.5","id":1,"nextFieldId":15,"useCurrentUserAsAuthor":true,"postContentTemplateEnabled":false,"postTitleTemplateEnabled":false,"postTitleTemplate":"","postContentTemplate":"","lastPageButton":null,"pagination":null,"firstPageCssClass":null,"is_active":"1","date_created":"2020-09-30 14:01:38","is_trash":"0","personalData":{"preventIP":false,"retention":{"policy":"retain","retain_entries_days":1},"exportingAndErasing":{"columns":{"ip":{"export":true,"erase":true},"source_url":{"export":true,"erase":true},"user_agent":{"export":true,"erase":true}},"enabled":true,"identificationField":"4"}},"notifications":{"5f748fc245c14":{"id":"5f748fc245c14","isActive":true,"to":"{admin_email}","name":"Admin Notification","event":"form_submission","toType":"email","subject":"New submission from {form_title}","message":"{all_fields}"}},"confirmations":{"5f748fc246018":{"id":"5f748fc246018","name":"Default Confirmation","isDefault":true,"type":"message","message":"Thanks for contacting us! We will get in touch with you shortly.","url":"","pageId":"","queryString":""}}}
             * */
            public GravityForm() { }
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public string title { get; set; }
            [DataMember]
            public string description { get; set; }
            [DataMember]
            public List<GravityField> fields { get; set; }
            public override string ToString()
            {
                var sb = new System.Text.StringBuilder();
                sb.Append("Form:{ID=");
                sb.Append(id);
                sb.Append(",");
                sb.Append("title=");
                sb.Append(title);
                sb.Append(",");
                sb.Append("description=");
                sb.Append(description);
                sb.Append(",");
                sb.Append("fields=");
                sb.Append(fields?.Count ?? 0);
                sb.Append("}");
                return sb.ToString();
            }


        }
        [DataContract]
        public class GravityField
        {
            public GravityField() { }
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string description { get; set; }
            //html, hidden, section, text, website, phone, number, date, time, textarea, select, checkbox, radio, name, address, fileupload, email, post_title, post_content, post_excerpt, post_tags, post_category, post_image, post_custom_field, captcha
            [DataMember]
            public string type { get; set; }
            [DataMember]
            public string label { get; set; }
            [DataMember]
            public List<GravityFieldInput> inputs { get; set; }
            public override string ToString()
            {
                var sb = new System.Text.StringBuilder();
                sb.Append("Field:{ID=");
                sb.Append(id);
                sb.Append(",");
                sb.Append("type=");
                sb.Append(type);
                sb.Append(",");
                sb.Append("label=");
                sb.Append(label);
                sb.Append(",");
                sb.Append("description=");
                sb.Append(description);
                sb.Append(",");
                sb.Append("input options=");
                sb.Append(inputs?.Count ?? 0);
                sb.Append("}");
                return sb.ToString();
            }

        }
        [DataContract]
        public class GravityFieldInput
        {
            public GravityFieldInput() { }
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string label
            {
                get; set;
            }
            [DataMember]
            public string name { get; set; }
            public override string ToString()
            {
                var sb = new System.Text.StringBuilder();
                sb.Append("Input:{ID=");
                sb.Append(id);
                sb.Append(",");
                sb.Append("label=");
                sb.Append(label);
                sb.Append(",");
                sb.Append("name=");
                sb.Append(name);
                sb.Append("}");
                return sb.ToString();
            }

        }

        [DataContract]
        public class GravityEntrySet
        {
            [DataMember]
            public int total_count { get; set; }
            [DataMember]
            public List<Dictionary<string, object>> entries { get; set; }
        }
        // This contains field IDs as keys, which are numeric, so the entry object has to be extracted from a dictionary
        [DataContract]
        public class GravityEntry
        {
            // http://jigsaw.kuliukas.com/wp-json/gf/v2/entries
            // http://jigsaw.kuliukas.com/wp-json/gf/v2/entries/1

            // {"total_count":1,"entries":[{"id":"1","form_id":"1","post_id":null,"date_created":"2020-09-30 14:17:14","date_updated":"2020-09-30 14:17:14","is_starred":"0","is_read":"0","ip":"124.170.43.203","source_url":"http:\/\/jigsaw.kuliukas.com\/form-page\/","user_agent":"Mozilla\/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit\/537.36 (KHTML, like Gecko) Chrome\/85.0.4183.102 Safari\/537.36","currency":"AUD","payment_status":null,"payment_date":null,"payment_amount":null,"payment_method":null,"transaction_id":null,"is_fulfilled":null,"created_by":"1","transaction_type":null,"status":"active","2.3":"Foo","2.6":"Bar","8.1":"Search engine","9":"30-40","12":"Member","13":["Finding a parent"],"14":"Just testing this out","6":"(044) 999-9452","4":"chris@kuliukas.com","2.2":"","2.4":"","2.8":"","10":"","8.2":"","8.3":"","11":"","7.1":"","7.2":"","7.3":"","7.4":"","7.5":"","7.6":""}]}
            // {"id":"1","form_id":"1","post_id":null,"date_created":"2020-09-30 14:17:14","date_updated":"2020-09-30 14:17:14","is_starred":"0","is_read":"0","ip":"124.170.43.203","source_url":"http:\/\/jigsaw.kuliukas.com\/form-page\/","user_agent":"Mozilla\/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit\/537.36 (KHTML, like Gecko) Chrome\/85.0.4183.102 Safari\/537.36","currency":"AUD","payment_status":null,"payment_date":null,"payment_amount":null,"payment_method":null,"transaction_id":null,"is_fulfilled":null,"created_by":"1","transaction_type":null,"status":"active","2.3":"Foo","2.6":"Bar","8.1":"Search engine","9":"30-40","12":"Member","13":["Finding a parent"],"14":"Just testing this out","6":"(044) 999-9452","4":"chris@kuliukas.com","2.2":"","2.4":"","2.8":"","10":"","8.2":"","8.3":"","11":"","7.1":"","7.2":"","7.3":"","7.4":"","7.5":"","7.6":""}
            private GravityForm form;
            private Dictionary<string, object> jsonObject;
            public GravityEntry(GravityForm _form, Dictionary<string, object> _jsonObject)
            {
                form = _form;
                jsonObject = _jsonObject;
                id = jsonObject.ContainsKey("id") && jsonObject["id"] != null ? Int32.Parse(jsonObject["id"].ToString()) : -1;

                form_id = jsonObject.ContainsKey("form_id") ? jsonObject["form_id"]?.ToString() : null;

                created_by = jsonObject.ContainsKey("created_by") && jsonObject["created_by"] != null ? Int32.Parse(jsonObject["created_by"].ToString()) : -1;

                date_created = jsonObject.ContainsKey("date_created") ? jsonObject["date_created"]?.ToString() : null;

                is_starred = (jsonObject.ContainsKey("is_starred") && jsonObject["is_starred"] != null ? Int32.Parse(jsonObject["is_starred"].ToString()) : 0) == 1;

                is_read = (jsonObject.ContainsKey("is_read") && jsonObject["is_read"] != null ? Int32.Parse(jsonObject["is_read"].ToString()) : 0) == 1;

                ip = jsonObject.ContainsKey("ip") ? jsonObject["ip"]?.ToString() : null;

                source_url = jsonObject.ContainsKey("source_url") ? jsonObject["source_url"]?.ToString() : null;

                post_id = jsonObject.ContainsKey("post_id") && jsonObject["post_id"] != null ? Int32.Parse(jsonObject["post_id"].ToString()) : -1;

                user_agent = jsonObject.ContainsKey("user_agent") ? jsonObject["user_agent"]?.ToString() : null;

                status = jsonObject.ContainsKey("status") ? jsonObject["status"]?.ToString() : null;

                currency = jsonObject.ContainsKey("currency") ? jsonObject["currency"]?.ToString() : null;

                payment_status = jsonObject.ContainsKey("payment_status") ? jsonObject["payment_status"]?.ToString() : null;

                payment_date = jsonObject.ContainsKey("payment_date") ? jsonObject["payment_date"]?.ToString() : null;

                payment_amount = jsonObject.ContainsKey("payment_amount") && jsonObject["payment_amount"] != null ? Decimal.Parse(jsonObject["payment_amount"].ToString()) : -1;

                transaction_id = jsonObject.ContainsKey("transaction_id") ? jsonObject["transaction_id"]?.ToString() : null;

                is_fulfilled = (jsonObject.ContainsKey("is_fulfilled") && jsonObject["is_fulfilled"] != null ? Int32.Parse(jsonObject["is_fulfilled"].ToString()) : 0) == 1;

                transaction_type = jsonObject.ContainsKey("transaction_type") && jsonObject["transaction_type"] != null ? Int32.Parse(jsonObject["transaction_type"].ToString()) : -1;

                if( form != null )
                {
                    foreach (var field in form.fields)
                    {
                        foreach (var kvp in jsonObject)
                        {
                            if (kvp.Key == field.id || kvp.Key.StartsWith(field.id + "."))
                            {
                                string value = "";

                                var listVal = kvp.Value as IEnumerable<object>;
                                if (listVal != null)
                                    value = listVal.Select(l => (l?.ToString()) ?? "").Aggregate((a, b) => a + "," + b);
                                else
                                    value = kvp.Value?.ToString();
                                if(field.type == "list" ) //label.Contains("Partici") )
                                {
                                    var fluff = value;
                                    int a = 0;
                                    a++;
                                    //kvp.Key = id 21

                                    // field.description = ""
                                    // field.id = "21"
                                    //field.inputs = null
                                    // field.label = "Course Participant Details"
                                    // field.@type = "list"
                                    // value = [{"First Name":"Blayde","Last Name":"Mager","Email":"blayde.mager@123456.wa.gov.au","Mobile":"1234567"}]

                                    var entryDictionaries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(value);

                                    int i = 101;
                                    foreach (var entryDictionary in entryDictionaries)
                                    {
                                        foreach (var subKVP in entryDictionary)
                                        {
                                            var subField = new GravityField() { description = "", id = i.ToString(), inputs = null, label = subKVP.Key.ToString(), type = "string" };
                                            formValues.Add(i.ToString(), new Tuple<GravityField, string, string>(subField, i.ToString(), subKVP.Value));
                                            i++;
                                        }
                                    }
                                }
                                if (formValues.ContainsKey(kvp.Key))
                                    throw new Exception(kvp.Key + " already exists");
                                else
                                    formValues.Add(kvp.Key, new Tuple<GravityField, string, string>(field, kvp.Key, value));
                            }
                        }
                    }
                }
                else
                {
                    foreach (var kvp in jsonObject)
                    {
                        string value = "";

                        var listVal = kvp.Value as IEnumerable<object>;
                        if (listVal != null)
                            value = listVal.Select(l => (l?.ToString()) ?? "").Aggregate((a, b) => a + "," + b);
                        else
                            value = kvp.Value?.ToString();

                        if (formValues.ContainsKey(kvp.Key))
                            throw new Exception(kvp.Key + " already exists");
                        else
                            formValues.Add(kvp.Key, new Tuple<GravityField, string, string>(null, kvp.Key, value));
                    }

                }
            }
            public override string ToString()
            {
                var sb = new System.Text.StringBuilder();
                sb.Append("Entry:{ID=");
                sb.Append(id);
                sb.Append(",");
                sb.Append("date_created=");
                sb.Append(date_created);
                sb.Append(",");
                sb.Append("is_read=");
                sb.Append(is_read);
                sb.Append(",");
                sb.Append("status=");
                sb.Append(status);
                sb.Append(",");
                sb.Append("values={");
                foreach (var f in formValues)
                {
                    sb.Append(f.Value.Item1.label);
                    sb.Append("=");
                    sb.Append(f.Value.Item2 ?? "");
                    sb.Append(",");
                }
                sb.Append("}");
                return sb.ToString();
            }

            public Dictionary<string, Tuple<GravityField, string, string>> formValues { get; set; } = new Dictionary<string, Tuple<GravityField, string, string>>();
            public IEnumerable<Tuple<GravityField, string, string>> formValuesOrdered => formValues.Values.ToList().OrderBy(v => Decimal.Parse(v.Item2)).ToList();
            public int id { get; set; }
            [DataMember]
            public string form_id { get; set; }
            [DataMember]
            public int created_by { get; set; }
            [DataMember]
            public string date_created { get; set; }
            [DataMember]
            public bool is_starred { get; set; }
            [DataMember]
            public bool is_read { get; set; }
            [DataMember]
            public string ip { get; set; }
            [DataMember]
            public string source_url { get; set; }
            [DataMember]
            public int post_id { get; set; }
            [DataMember]
            public string user_agent { get; set; }
            [DataMember]
            public string status { get; set; }
            [DataMember]
            public string currency { get; set; }
            [DataMember]
            public string payment_status { get; set; }
            [DataMember]
            public string payment_date { get; set; }
            [DataMember]
            public decimal payment_amount { get; set; }
            [DataMember]
            public string transaction_id { get; set; }
            [DataMember]
            public bool is_fulfilled { get; set; }
            [DataMember]
            public int transaction_type { get; set; }
        }
    }
}
