using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WFA_GitAutomateTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //--We need to do some task
            //--We NEED TO DO SOME CHECK FIRST
            if(tbPath.Text == "")
            {
                MessageBox.Show("Please browse a proper folder path");
                return;
            }

            if(checkBox8.Checked == true)
            {
                if(CB_List.Text == "" || CB_min_or_hour.Text == "")
                {
                    MessageBox.Show("Please fill with proper value to repeat task");
                    return;
                }
                int parsedValue;
                if (!int.TryParse(CB_List.Text, out parsedValue))
                {
                    MessageBox.Show("In Repeate task : chose only numbers");
                    return;
                }

            }


            //--If file is present the delete the files 
            string FileName = "Info.xml";
            string FolderDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path1 = FolderDirectory + @"\" + FileName;
            if (File.Exists(path1))
            {
                //Delete the files
                File.Delete(path1);
            }

            string nishantFile = "NishantKarkiFile.xml";
            string path2 = FolderDirectory + @"\" + nishantFile;
            if (File.Exists(path2))
            {
                //Delete the files
                File.Delete(path2);
            }


            //--Now lets make the NishantKarkiFile.xml file
            GenerateXMLFile();

            //--Now lets generate next info.xml file
            GenerateInfoXMLFile();


            //--Execute bat file 
            // /*
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = FolderDirectory + @"\scheduler.bat"; // "C:\\Watcher\\Cleanup.bat";
            proc.StartInfo.WorkingDirectory = FolderDirectory + @"\"; //"C:\\Watcher";
            proc.Start();


            //*/

            //  MessageBox.Show("Finish :\n "+ FolderDirectory + @"\scheduler.bat");
            MessageBox.Show("Task Creation success!");
            this.Close();

        } //Close of main fxn

        /// <summary>
        /// Functions helps to generate xml files 
        /// </summary>
        public void GenerateXMLFile()            
        {
            XmlDocument xmlDoc = new XmlDocument();
            //XmlWriter xw = new XmlWriter();
            //lets create an xml document using a string in xml formate


            XmlNode mainNode = xmlDoc.CreateElement("Task");
                XmlAttribute attribute = xmlDoc.CreateAttribute("xmlns");
                attribute.Value = "http://schemas.microsoft.com/windows/2004/02/mit/task";
                mainNode.Attributes.Append(attribute);
                XmlAttribute attributeMain2 = xmlDoc.CreateAttribute("version");
                attributeMain2.Value = "1.2";            
                mainNode.Attributes.Append(attributeMain2);
            xmlDoc.AppendChild(mainNode);


             //--For registrationInfo
            XmlNode regInfo = xmlDoc.CreateElement("RegistrationInfo");
            //identifier.InnerText = "MainForm";//This is for identiying main form

            //Description
            XmlNode desc = xmlDoc.CreateElement("Description");
            desc.InnerText = "This is a task for auto git schedule";//This is for identiying main form
            regInfo.AppendChild(desc);

            //URL
            XmlNode URI = xmlDoc.CreateElement("URI");
            URI.InnerText = @"\a_test_task_import_without SID";//URI
            regInfo.AppendChild(URI);
                                                                   
            mainNode.AppendChild(regInfo);

            //--This one is for triggers 
            XmlNode Triggers = xmlDoc.CreateElement("Triggers");
            //identifier.InnerText = "MainForm";//This is for identiying main form
            mainNode.AppendChild(Triggers);

            //Description
            XmlNode CalendarTrigger = xmlDoc.CreateElement("CalendarTrigger");          
            Triggers.AppendChild(CalendarTrigger);

            if(checkBox8.Checked ==true && CB_List.Text != "" && CB_min_or_hour.Text != "")
            { 

            XmlNode Repetition = xmlDoc.CreateElement("Repetition");
            CalendarTrigger.AppendChild(Repetition);

           if(CB_List.Text !="" && CB_min_or_hour.Text != "")
            {
                string intervalString = "";
                if(CB_min_or_hour.Text == "Minute")
                {
                    intervalString = "PT" + CB_List.Text + "M";
                }
                else
                {
                    intervalString = "PT" + CB_List.Text + "H";
                }

            XmlNode Interval = xmlDoc.CreateElement("Interval");
            Interval.InnerText = intervalString;
            Repetition.AppendChild(Interval);
            }

            }//Close of if checkbox8.checked


            string startBoundryVlaue = DTP_Date.Text + "T" + DTP_Time.Text;
            //StartBoundary
            XmlNode StartBoundary = xmlDoc.CreateElement("StartBoundary");
            StartBoundary.InnerText = startBoundryVlaue;
            CalendarTrigger.AppendChild(StartBoundary);

            XmlNode Enabled = xmlDoc.CreateElement("Enabled");
            Enabled.InnerText = "true";
            CalendarTrigger.AppendChild(Enabled);

            //ScheduleByWeek          y
            XmlNode ScheduleByWeek = xmlDoc.CreateElement("ScheduleByWeek");
           // ScheduleByWeek.InnerText = startBoundryVlaue;
            CalendarTrigger.AppendChild(ScheduleByWeek);

            XmlNode DaysOfWeek = xmlDoc.CreateElement("DaysOfWeek");
            // DaysOfWeek.InnerText = "true";
            ScheduleByWeek.AppendChild(DaysOfWeek);

            if (checkBox1.Checked == true)
            {
                //Sunday is checked
                XmlNode Sunday = xmlDoc.CreateElement("Sunday");
                DaysOfWeek.AppendChild(Sunday);
            }
             if (checkBox2.Checked == true)
            {
                //mon is checked
                XmlNode Monday = xmlDoc.CreateElement("Monday");
                DaysOfWeek.AppendChild(Monday);
            }
             if (checkBox3.Checked == true)
            {
                //tues is checked
                XmlNode Tuesday = xmlDoc.CreateElement("Tuesday");
                DaysOfWeek.AppendChild(Tuesday);
            }
             if (checkBox4.Checked == true)
            {
                //wed is checked
                XmlNode Wednesday = xmlDoc.CreateElement("Wednesday");
                DaysOfWeek.AppendChild(Wednesday);
            }
             if (checkBox5.Checked == true)
            {
                //thurs is checked
                XmlNode Thursday = xmlDoc.CreateElement("Thursday");
                DaysOfWeek.AppendChild(Thursday);
            }
             if (checkBox6.Checked == true)
            {
                //fri is checked
                XmlNode Friday = xmlDoc.CreateElement("Friday");
                DaysOfWeek.AppendChild(Friday);
            }
             if (checkBox7.Checked == true)
            {
                //Sat is checked
                XmlNode Saturday = xmlDoc.CreateElement("Saturday");
                DaysOfWeek.AppendChild(Saturday);
            }


            XmlNode WeeksInterval = xmlDoc.CreateElement("WeeksInterval");
            WeeksInterval.InnerText = "1";
            ScheduleByWeek.AppendChild(WeeksInterval);

            //--Close of triggers
            //--Principles 

            XmlNode Principals = xmlDoc.CreateElement("Principals");
            //DaysOfWeek.InnerText = "1";
            mainNode.AppendChild(Principals);

            //Principal
            XmlNode Principal = xmlDoc.CreateElement("Principal");
            XmlAttribute principalAttr = xmlDoc.CreateAttribute("id");
            principalAttr.Value = "Author";
            Principal.Attributes.Append(principalAttr);                                           
            //DaysOfWeek.InnerText = "1";
            Principals.AppendChild(Principal);

            //--Log on type                            
            XmlNode LogonType = xmlDoc.CreateElement("LogonType");
            LogonType.InnerText = "InteractiveToken";
            Principal.AppendChild(LogonType);

            //RunLavel
            XmlNode RunLevel = xmlDoc.CreateElement("RunLevel");
            RunLevel.InnerText = "LeastPrivilege";
            Principal.AppendChild(RunLevel);

            //Principals close

            //--Now settings..
            XmlNode Settings = xmlDoc.CreateElement("Settings");
            //DaysOfWeek.InnerText = "1";
            mainNode.AppendChild(Settings);


            //--MultipleInstancesPolicy                           
            XmlNode MultipleInstancesPolicy = xmlDoc.CreateElement("MultipleInstancesPolicy");
            MultipleInstancesPolicy.InnerText = "IgnoreNew";
            Settings.AppendChild(MultipleInstancesPolicy);

            //disallow..
            XmlNode DisallowStartIfOnBatteries = xmlDoc.CreateElement("DisallowStartIfOnBatteries");
            DisallowStartIfOnBatteries.InnerText = "false";
            Settings.AppendChild(DisallowStartIfOnBatteries);

            //--StopIfGoingOnBatteries
            XmlNode StopIfGoingOnBatteries = xmlDoc.CreateElement("StopIfGoingOnBatteries");
            StopIfGoingOnBatteries.InnerText = "false";
            Settings.AppendChild(StopIfGoingOnBatteries);

            //--AllowHardTerminate
            XmlNode AllowHardTerminate = xmlDoc.CreateElement("AllowHardTerminate");
            AllowHardTerminate.InnerText = "true";
            Settings.AppendChild(AllowHardTerminate);

            //--    StartWhenAvailable
            XmlNode StartWhenAvailable = xmlDoc.CreateElement("StartWhenAvailable");
            StartWhenAvailable.InnerText = "false";
            Settings.AppendChild(StartWhenAvailable);

            //--                RunOnlyIfNetworkAvailable
            XmlNode RunOnlyIfNetworkAvailable = xmlDoc.CreateElement("RunOnlyIfNetworkAvailable");
            RunOnlyIfNetworkAvailable.InnerText = "false";
            Settings.AppendChild(RunOnlyIfNetworkAvailable);

            //--IdleSettings
            XmlNode IdleSettings = xmlDoc.CreateElement("IdleSettings");
           // IdleSettings.InnerText = "false";
            Settings.AppendChild(IdleSettings);

            //--Inside  IdleSettings
            XmlNode StopOnIdleEnd = xmlDoc.CreateElement("StopOnIdleEnd");
             StopOnIdleEnd.InnerText = "true";
            IdleSettings.AppendChild(StopOnIdleEnd);

            //--Inside  IdleSettings
            XmlNode RestartOnIdle = xmlDoc.CreateElement("RestartOnIdle");
            RestartOnIdle.InnerText = "false";
            IdleSettings.AppendChild(RestartOnIdle);


            //--Within settings 
            //--AllowHardTerminate
            XmlNode AllowStartOnDemand = xmlDoc.CreateElement("AllowStartOnDemand");
            AllowStartOnDemand.InnerText = "true";
            Settings.AppendChild(AllowStartOnDemand);

            //--AllowHardTerminate
            XmlNode EnabledSet = xmlDoc.CreateElement("Enabled");
            EnabledSet.InnerText = "true";
            Settings.AppendChild(EnabledSet);

            //--Hidden
            XmlNode Hidden = xmlDoc.CreateElement("Hidden");
            Hidden.InnerText = "false";
            Settings.AppendChild(Hidden);

            //--RunOnlyIfIdle
            XmlNode RunOnlyIfIdle = xmlDoc.CreateElement("RunOnlyIfIdle");
            RunOnlyIfIdle.InnerText = "false";
            Settings.AppendChild(RunOnlyIfIdle);

            //--WakeToRun
            XmlNode WakeToRun = xmlDoc.CreateElement("WakeToRun");
            WakeToRun.InnerText = "false";
            Settings.AppendChild(WakeToRun);

            //--AllowHardTerminate
            XmlNode ExecutionTimeLimit = xmlDoc.CreateElement("ExecutionTimeLimit");
            ExecutionTimeLimit.InnerText = "PT72H";
            Settings.AppendChild(ExecutionTimeLimit);

            //--AllowHardTerminate
            XmlNode Priority = xmlDoc.CreateElement("Priority");
            Priority.InnerText = "7";
            Settings.AppendChild(Priority);

            //--Actions sections
            XmlNode Actions = xmlDoc.CreateElement("Actions");
            XmlAttribute action_attr = xmlDoc.CreateAttribute("Context");
            action_attr.Value = "Author";
            Actions.Attributes.Append(action_attr);           
            mainNode.AppendChild(Actions);

            //exec
            XmlNode Exec = xmlDoc.CreateElement("Exec");
            // Priority.InnerText = "7";
            Actions.AppendChild(Exec);

            string batFilePath = "";
            string FolderDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            batFilePath = FolderDirectory + @"\git_redirect.bat";

            XmlNode Command = xmlDoc.CreateElement("Command");
            Command.InnerText = batFilePath;
            Exec.AppendChild(Command);


            XmlNode WorkingDirectory = xmlDoc.CreateElement("WorkingDirectory");
            WorkingDirectory.InnerText = FolderDirectory+@"\";
            Exec.AppendChild(WorkingDirectory);

            //--Now create a file
            //And save it
            string FileName = "NishantKarkiFile.xml";
            string path = FolderDirectory + @"\" + FileName;
            xmlDoc.Save(path);//Save the xml file to the path

        }

        public void GenerateInfoXMLFile()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //XmlWriter xw = new XmlWriter();
            //lets create an xml document using a string in xml formate


            XmlNode mainNode = xmlDoc.CreateElement("Info");
            xmlDoc.AppendChild(mainNode);


            //TaskName    
            XmlNode taskName = xmlDoc.CreateElement("taskName");
            taskName.InnerText = "my_test_task";
            mainNode.AppendChild(taskName);

            //--Repo
            XmlNode repoDir = xmlDoc.CreateElement("repoDir");
            repoDir.InnerText = tbPath.Text;
            mainNode.AppendChild(repoDir);

            //--repoDirUnix
            XmlNode repoDirUnix = xmlDoc.CreateElement("repoDirUnix");           
            mainNode.AppendChild(repoDirUnix);

            //--save the file to info.xml
            string FileName = "Info.xml";
            string FolderDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = FolderDirectory + @"\" + FileName;
            xmlDoc.Save(path);//Save the xml file to the path



        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //This brose the path to th folder

            DialogResult result = FBD_FIND.ShowDialog();
            if (result == DialogResult.OK)
            {
                //
                // The user selected a folder and pressed the OK button.
                // We print the number of files found.
                //
               // string[] files = Directory.GetFiles(FBD_FIND.SelectedPath);
               // MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                tbPath.Text = FBD_FIND.SelectedPath;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnApply.Enabled = false;
            //--Lets set the formate for date time picker DTP_Date
            DTP_Date.Format = DateTimePickerFormat.Custom;
            DTP_Date.CustomFormat = "yyyy-MM-dd"; //"MMMM dd, yyyy -  dddd";

            DTP_Time.Format = DateTimePickerFormat.Custom;
            DTP_Time.CustomFormat = "HH:mm:ss"; //"MMMM dd, yyyy -  dddd";

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            enableBtnApply();
        }

        public void enableBtnApply()
        {
            if ((checkBox1.Checked == true) || (checkBox2.Checked == true) || (checkBox3.Checked == true) || (checkBox4.Checked == true) || (checkBox5.Checked == true) || (checkBox6.Checked == true) || (checkBox7.Checked == true))
            {
                btnApply.Enabled = true;
            }
            else
            {
                btnApply.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            enableBtnApply();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            enableBtnApply();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            enableBtnApply();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            enableBtnApply();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            enableBtnApply();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            enableBtnApply();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox8.Checked == true)
            {
                CB_List.Enabled = true;
                CB_min_or_hour.Enabled = true;
            }
            else
            {
                CB_List.Enabled = false;
                CB_min_or_hour.Enabled = false;
            }
        }

        private void CB_List_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void CB_List_TextChanged(object sender, EventArgs e)
        {

            //Vlaue is changes we need to check it is greate then 0 and upto 24
            string value = CB_List.Text;

            Regex regex = new Regex("^[0-9]+$");  //(@"^\d$");

            if ( !regex.IsMatch(value))
            {
                MessageBox.Show("Please input only numbers here");
                CB_List.Text = "";


            }
            else
            {
               

                //Matched only numbers 
                int number = int.Parse(value);
                if (number <= 0 || number >= 25)
                {
                    //Error
                    MessageBox.Show("Please enter number from 1 upto 24 value only");
                    CB_List.Text = "";
                }


            }

        }
    }
}
