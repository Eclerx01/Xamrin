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
using static Android.Widget.AdapterView;

namespace Assignment4
{
    [Activity(Label = "UserListActivity")]
    public class UserListActivity : Activity
    {
        public SearchView mySearch;
        ListView listView1;
        ArrayList arr_list = new ArrayList();
        List<string> userArray = new List<string>();
        string userN;
        List<UserObject> myUserList = new List<UserObject>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.user_list_layout);
            listView1 = FindViewById<ListView>(Resource.Id.listViewUser);
            mySearch = FindViewById<SearchView>(Resource.Id.searchView1);

            DBHelper myDBHelper = new DBHelper(this);

            ICursor result = myDBHelper.getUserList();

            while (result.MoveToNext())
            {
                //userN = result.GetString(result.GetColumnIndexOrThrow("f_name"));
                //System.Console.WriteLine(" The name of user is " + userN);

                //arr_list.Add(result.GetString(result.GetColumnIndex("f_name")));

                myUserList.Add(new UserObject(result.GetString(result.GetColumnIndex("f_name")), result.GetString(result.GetColumnIndex("age")), Resource.Drawable.dd));

            }
            CustomAdaptor adapter = new CustomAdaptor(this, myUserList); // Context
            listView1.Adapter = adapter;
            listView1.ItemClick += ListView1_ItemClick;
            mySearch.QueryTextChange += MySearch_QueryTextChange;
        }
        private void MySearch_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            //var mySearchValue = e.NewText;
            if (string.IsNullOrWhiteSpace(e.NewText))
            {
                CustomAdaptor myAdapter = new CustomAdaptor(this, myUserList);
                listView1.Adapter = myAdapter;
            }
            else
            {
                CustomAdaptor myAdapter = new CustomAdaptor(this, myUserList.Where(us => us.name.StartsWith(e.NewText)).ToList());
                listView1.Adapter = myAdapter;
            }
        }
        private void ListView1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var Index = e.Position;
            var uservalue = myUserList[Index];
            Intent userinfo_Screen = new Intent(this, typeof(userdetails));
            userinfo_Screen.PutExtra("userName", uservalue.name);
            StartActivity(userinfo_Screen);
        }



    }
}