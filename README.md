# GravityFormsAdapter
Download Gravity forms via REST to local JSON files or an SQL Server.

Gravity Forms is a nice plugin for WordPress that allows a non-technical user to easily create customized forms and publish them to WordPress. For organizations this capability is very useful to collect structured data anonymously.

The data that gets submitted via these Gravity Forms are then stored within the WordPress database, usually a MySQL database stored within a web host in the cloud.


A common use case for these forms is to allow data collection that then needs to feed back into the organization: The form data represents structured information that a user is submitting so that it can be processed by someone within the organization, but by itself Gravity Forms relies on a staff member within the organization to check for new submissions or respond to a notification that a submission has been entered and to then process the information in the form.

When there are multiple forms that need to be processed by multiple individuals, the form data needs some sort of workflow-type processing involving more than one staff member, or the form data is important to the business and needs to be collected, tracked and processed in a systematic way, this is too ad-hoc and there is a need for the data to be captured by a larger organization-wide system so that it can be controlled. Such systems usually take the form of an on-premises database.


Since the web-host running WordPress and Gravity Forms will often be on the cloud and set-up for simplicity and security to serve requests via a web-browser only, and the organization's database is on-premises and behind a firewall that is not set-up to serve requests remotely, the form data ends up siloed from the organization without expertise and time spent on both ends.

This tool simply fetches the gravity forms data via the REST API, downloading the structured form data securely via a web request that all WordPress hosts can respond to, and stores it locally as a JSON file or in a SQL Server database, so that it can be processed by the organization using its own internal systems.

Simply enable the Gravity Forms REST API in WordPress, generating a Consumer Secret and Consumer Key. Plug these secret values and the URL of the WordPress site running Gravity Forms into this adapter tool, and it will download and store the forms data. If a SQL Server connection string is entered the data will be saved directly into a SQL Server database. Also if the adapter tool is put in Auto-run mode it will start up, check for new data, download it and close, allowing the adapter to be simply and effectively used as an automated process, eliminating the need to check the WordPress site for new form submissions.
