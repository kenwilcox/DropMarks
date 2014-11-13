using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Procurios.Public;

namespace DropMarks
{
  public partial class frmJSON : Form
  {
    private Hashtable ht;

    public frmJSON()
    {
      InitializeComponent();
    }

    private void frmJSON_Load(object sender, EventArgs e)
    {
      double org = 1290059194;
      DateTime date = DateTimeEx.ParseUnixTimestamp(org);
      //MessageBox.Show(date.ToString());
      double d = DateTimeEx.ToUnixTimestamp(date);

      if (org == d) MessageBox.Show("Matches!");
    }

    private void frmJSON_LoadBoolean(object sender, EventArgs e)
    {
      ht = new Hashtable();
      ht["BooleanFalse"] = "0";
      ht["BooleanTrue"] = "1";

      Boolean b = ht.ParseBoolean("BooleanTrue");
      MessageBox.Show("BooleanTrue = " + b.ToString());
    }

    private void frmJSON_LoadOld(object sender, EventArgs e)
    {
      //string data = "{\"keyID\":\"2842E007223A4554A01913E5BA33AE6D\",\"locationKey\":\"\",\"encrypted\":\"U2FsdGVkX1+I6Aih5tkXCJ8IiA8GY1tpIywAcTI83N4=\u0000\",\"typeName\":\"system.folder.Regular\",\"openContents\":{\"securityLevel\":\"SL5\",\"contentsHash\":\"5ddc725b\"},\"location\":\"\",\"uuid\":\"BD68C045E0C04D33908FCD695C4FC681\",\"updatedAt\":1225497544,\"createdAt\":1225497544,\"title\":\"Imported\"}";
      //string data = "[\"One\",\"Two\",\"Three\",\"Four\",\"Five\",\"Six\",\"Seven\",\"Eight\",\"Nine\",\"Ten\",\"Eleven\",\"Twelve\",\"Programming\",\"Prog\",\"iPhone\",\"iPad\",\"iOS\",\"Mac OS X\",\"Photo\",\"Photography\"]";
      //DropMark dm = new DropMark("Google", "http://www.google.com");
      //string data = JSON.JsonEncode(dm.ToHashTable());
      
      string data = "{\"Uuid\":\"472b34a8-6812-468f-a300-145b4285b8e5\",\"Title\":\"This is my Title\",\"Url\":\"http://www.google.com\",\"CreatedAt\":1309281937177,\"UpdatedAt\":1309281937177}";
      ht = (Hashtable)JSON.JsonDecode(data);
      //ArrayList al = (ArrayList)JSON.JsonDecode(data);
      //MessageBox.Show(ht.ToString());
      foreach(string key in ht.Keys)
      //foreach (string key in al)
      {
        //MessageBox.Show(String.Format("[{0}] = {1}", key, ht[key]));
        lbKeys.Items.Add(key);
      }
      // Encode the ArrayList
      //String newdata = JSON.JsonEncode(al);
      //if (data.Equals(newdata))
      //  MessageBox.Show(data);
    }

    private void lbKeys_SelectedIndexChanged(object sender, EventArgs e)
    {
      string key = (string)lbKeys.SelectedItem;
      string data = ht[key].ToString();
      if (key.Contains("At"))
      {
        DateTime dt = DateTimeEx.ParseUnixTimestamp(Double.Parse(data));
        data = dt.ToString();
      }
      txtValue.Text = data;
    }
  }
}
