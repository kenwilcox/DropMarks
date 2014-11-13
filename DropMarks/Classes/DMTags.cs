using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Bson;
//using Newtonsoft.Json.Converters;
using System.IO;
//using System.Runtime.Serialization;
using System.Collections;
using Procurios.Public;

namespace DropMarks
{
  /// <summary>
  /// Class to hold any/all tags that are used
  /// </summary>
  public class DMTags/*: Component, IListSource, INotifyPropertyChanged*/
  {
    private static DMTags _instance = new DMTags();
    private static List<string> _items;
    private static List<string> _orgItems;

    /// <summary>
    /// You can't create me neener neener neener
    /// </summary>
    private DMTags()
    {
      _items = new List<string>();
      _orgItems = new List<string>();
    }
    
    //private DMTags(IContainer container)
    //{
    //  container.Add(this);
    //}

    public static DMTags Instance
    {
      get { return _instance; }
    }

    public void Add(string item)
    {
      if (_items.IndexOf(item) < 0)
      {
        _items.Add(item);
        //OnPropertyChanged("Item");
      }
    }

    public void Remove(string item)
    {
      if (_items.IndexOf(item) <= 0)
      {
        _items.Remove(item);
        //OnPropertyChanged("Item");
      }
    }

    public List<string> Items
    {
      get { return _items; }
    }

    public void Save(string fileName)
    {
      //throw new NotImplementedException("Ken you need to fix this");
      //string output = JsonConvert.SerializeObject(_items);
      /*
      JsonSerializer serializer = new JsonSerializer();
      serializer.Converters.Add(new JavaScriptDateTimeConverter());
      serializer.NullValueHandling = NullValueHandling.Ignore;
      using (StreamWriter sw = new StreamWriter(fileName))
      using (JsonWriter writer = new JsonTextWriter(sw))
      {
        serializer.Serialize(writer, _items);
      }
      */

      // Just so Drop doesn't go haywire only save if the
      // current list is different from the original one
      if (!_orgItems.SequenceEqual(_items))
      {
        //string data = JsonConvert.SerializeObject(_items);
        ArrayList al = new ArrayList(_items);
        string data = JSON.JsonEncode(al);
        using (StreamWriter sw = new StreamWriter(fileName))
        {
          sw.Write(data);
          sw.Close();
        }
      }
    }

    public void Load(string fileName)
    {
      string data;
      using (StreamReader sr = new StreamReader(fileName))
      {
        data = sr.ReadToEnd();
        //_items = JsonConvert.DeserializeObject<List<string>>(data);
        // Just load it again - should be as fast as a copy
        //_orgItems =  JsonConvert.DeserializeObject<List<string>>(data);
        sr.Close();
      }
      ArrayList al = (ArrayList)JSON.JsonDecode(data);
      _items.Clear();
      _orgItems.Clear();
      foreach (string item in al)
      {
        _items.Add(item);
        _orgItems.Add(item);
      }
    }

    /*
    bool IListSource.ContainsListCollection
    {
      get { return false; }
    }

    System.Collections.IList IListSource.GetList()
    {
      BindingList<string> bl = new BindingList<string>();
      if (!this.DesignMode)
      {
        foreach (string item in _items)
          bl.Add(item);
      }
      return bl;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
      OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    private void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (null != PropertyChanged)
      {
        PropertyChanged(this, e);
      }
    }
    */
  }
}
