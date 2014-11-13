using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using Procurios.Public;

namespace DropMarks
{
  public class DropMarks : SortableBindingList<DropMark>
  {
    public bool UuidExists(Guid uuid)
    {
      bool ret = false;
      foreach (DropMark dm in this)
      {
        if (dm.Uuid == uuid)
        {
          ret = true;
          break;
        }
      }
      return ret;
    }

    public bool URLExists(string url)
    {
      bool ret = false;
      foreach (DropMark dm in this)
      {
        if (dm.Url == url)
        {
          ret = true;
          break;
        }
      }
      return ret;
    }

    public new void Add(DropMark item)
    {
      // Compare the Uuid's with ones we already have
      // If there is one that's the same DON'T add another one      
      if (!UuidExists(item.Uuid))
        if (!URLExists(item.Url))
          base.Add(item);
    }

    public Boolean Load()
    {
      Boolean ret = true;
      string[] files = Directory.GetFiles(".", "*.dropmark");
      if (files.Length <= 0) ret = false;
      // Find all files in DropBox Dir
      foreach (string fileName in files)
      {
        using (StreamReader sr = new StreamReader(fileName))
        {
          string data = sr.ReadToEnd();
          //DropMark dm = JsonConvert.DeserializeObject<DropMark>(data);
          Hashtable ht = (Hashtable)JSON.JsonDecode(data);
          DropMark dm = new DropMark(ht);
          this.Add(dm);
          sr.Close();
        }
      }

      return ret;
    }

    public void Save()
    {
      foreach (DropMark dm in this)
      {
        //*
        //string data = JsonConvert.SerializeObject(dm);
        if (dm.IsDirty)
        {
          string data = JSON.JsonEncode(dm.ToHashTable());
          using (StreamWriter sw = new StreamWriter(dm.GetUuid() + ".dropmark"))
          {
            sw.Write(data);
            sw.Close();
          }
        }
        //*/
        /*
        JsonSerializer serializer = new JsonSerializer();
        serializer.Converters.Add(new JavaScriptDateTimeConverter());
        //serializer.Converters.Add(new IsoDateTimeConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;
        using (StreamWriter sw = new StreamWriter(dm.Uuid + ".dropmark"))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
          serializer.Serialize(writer, dm);
        }
        //*/
      }
    }

  }
}
