using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Assignment4
{
    [Activity(Label = "DETAILS")]
    public class userdetails : Activity
    {
        String valueFromnUserList;
        DBHelper myDB;
        ICursor cs;
        ListView mylist;

        ArrayList listitem = new ArrayList();
        ArrayAdapter myAdapter;
        List<UserObject> myUserList = new List<UserObject>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.detail);


            mylist = FindViewById<ListView>(Resource.Id.listView1); //listview

            valueFromnUserList = Intent.GetStringExtra("userName");
            System.Console.WriteLine("Name from userlist ---> " + valueFromnUserList);

            myDB = new DBHelper(this);
            cs = myDB.SelectMyValues(valueFromnUserList);


            listitem.Add(cs.GetString(cs.GetColumnIndex("age")));
            listitem.Add(cs.GetString(cs.GetColumnIndex("email")));
            listitem.Add(cs.GetString(cs.GetColumnIndex("password")));

            myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, listitem);

            mylist.Adapter = myAdapter;
        }
    }
}

