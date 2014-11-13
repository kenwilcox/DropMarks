using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
using System.Collections;
using Procurios.Public;

namespace DropMarks
{
  public class DropMark
  {
    private string _title;
    private string _url;
    private DateTime _created;
    private DateTime _updated;
    private Guid _uuid;
    private string _tags;
    private string _comment;
    private bool _dirty;

    //private string[] _hack;

    public DropMark(string Title, string Url)
    {
      _title = Title;
      _url = Url;
      _created = DateTime.Now;
      _updated = DateTime.Now;
      _uuid = Guid.NewGuid();
      _tags = String.Empty;
      _comment = String.Empty;
      //_hack = new string[];
      _dirty = false;
    }

    public DropMark(Hashtable ht)
    {
      // Set the values from the JSON file providing defaults
      // if none are found
      _title = ht.ParseString("Title", "No Title");
      _url = ht.ParseString("Url", "No Url");
      _created = ht.ParseDate("CreatedAt", DateTime.Now);
      _updated = ht.ParseDate("UpdatedAt", DateTime.Now);
      _uuid = ht.ParseGuid("Uuid", Guid.NewGuid());
      _tags = ht.ParseString("Tags");
      _comment = ht.ParseString("Comment");
      _dirty = false;

      //string tags = ht.ParseString("Tags");
      //_hack = tags.Split(',');

      //DateTime dt = new DateTime();

      //if (ht.ContainsKey("Title"))
      //  _title = ht["Title"].ToString();
      //else
      //  _title = "No Title";

      //if (ht.ContainsKey("Url"))
      //  _url = ht["Url"].ToString();
      //else
      //  _url = "No Url";

      // If we have no title and no url - what's the point?
      /*
        DateTime dt = new DateTime().ParseUnixTimestamp(Double.Parse(data));
        data = dt.ToString();
      */
      //if (ht.ContainsKey("CreatedAt"))
      //  _created = dt.ParseUnixTimestamp(Double.Parse(ht["CreatedAt"].ToString()));
      //else
      //  _created = DateTime.Now;
      //if (ht.ContainsKey("UpdatedAt"))
      //  _updated = dt.ParseUnixTimestamp(Double.Parse(ht["UpdatedAt"].ToString()));
      //else
      //  _updated = DateTime.Now;
      //if (ht.ContainsKey("Uuid"))
      //  _uuid = new Guid(ht["Uuid"].ToString());
      //else
      //  _uuid = Guid.NewGuid();

    }

    /*
    public String ParseString(Hashtable ht, string key, string defaultValue)
    {
      string ret;
      if (ht.ContainsKey(key))
        ret = ht[key].ToString();
      else
        ret = defaultValue;
      return ret;
    }
    */
    // This consturctor should only be called to deserialize dropmark objects
    /*
    public DropMark(string Title, string Url, DateTime CreatedAt, DateTime UpdatedAt, Guid Uuid)
    {
      _title = Title;
      _url = Url;
      _created = CreatedAt;
      _updated = UpdatedAt;
      _uuid = Uuid;
    }
    */

    //public Guid Uuid
    //{
    //  get { return _uuid; }
    //}
    
    // internal is not public, so this item will not show up on a grid
    // but I can still use it like normal properties
    internal Guid Uuid
    {
      get { return _uuid; }
    }

    internal Boolean IsDirty
    {
      get { return _dirty; }
    }

    public string GetUuid()
    {
      return _uuid.ToString().ToUpper();
    }

    public String Title
    {
      get { return _title; }
      set 
      {
        if (value != _title)
        {
          _title = value;
          SetUpdated();
        }
      }
    }

    private void SetUpdated()
    {
      _updated = DateTime.Now;
      _dirty = true;
    }

    public String Url
    {
      get { return _url; }
      set
      {
        if (value != _url)
        {
          _url = value;
          SetUpdated();
        }
      }
    }

    public DateTime CreatedAt
    {
      get { return _created; }
    }

    public DateTime UpdatedAt
    {
      get { return _updated; }
      // I don't want anyone to call this directly
      //set { _updated = value; }
    }

    public String Tags
    {
      get { return _tags; }
      // Not sure how I want to set them yet...
    }

    public String Comment
    {
      get { return _comment; }
      // We've updated the comment, update the time too
      set 
      {
        if (value != _comment)
        {
          _comment = value;
          SetUpdated();
        }
      }
    }

    // A DAMN hell of a lot easier than normal serialization
    // Just fill the properties we care about into a Hashtable
    // The JSON class will happily write it all
    public Hashtable ToHashTable()
    {
      Hashtable ht = new Hashtable();
      ht["Uuid"] = _uuid.ToString();
      ht["Title"] = _title;
      ht["Url"] = _url;
      ht["CreatedAt"] = DateTimeEx.ToUnixTimestamp(_created).ToString();
      ht["UpdatedAt"] = DateTimeEx.ToUnixTimestamp(_updated).ToString();
      if (_tags.Length > 0) ht["Tags"] = _tags;
      if (_comment.Length > 0) ht["Comment"] = _comment;
      return ht;
    }
  }
}
