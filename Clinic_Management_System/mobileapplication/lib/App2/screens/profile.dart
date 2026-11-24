
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../provider/UserProvider.dart';
import 'form.dart';
import 'homePage.dart';
import 'loginscreen.dart';

class Profile extends StatelessWidget {

  const Profile({super.key});

  @override
  Widget build(BuildContext context) {
    final userProvider = context.watch<Userprovider>();

    return Scaffold(
        drawer: Drawer(
          child: Column(
            children: [
              UserAccountsDrawerHeader(
                accountName: Text(userProvider.username),
                accountEmail: Text(userProvider.email),
                currentAccountPicture: const CircleAvatar(
              backgroundColor: Colors.grey,
              child: Icon(
                Icons.person,
                size: 40,
                color: Colors.white,
                      ),
                ),
                decoration: const BoxDecoration(
                  color: Colors.blue,
                ),
              ),
              ListTile(
                leading: const Icon(Icons.home),
                title: const Text('Home'),
                onTap:(){
                  Navigator.pop(context);
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => Homepage(),
                    ),
                  );
                },
              ),
              ListTile(
                leading: const Icon(Icons.person),
                title: const Text('Update Profile'),
                onTap: () {
                  Navigator.pop(context);
                },
              ),

              const Spacer(),

              const Divider(),

              ListTile(
                leading: const Icon(Icons.logout, color: Colors.red),
                title: const Text(
                  'Logout',
                  style: TextStyle(color: Colors.red),
                ),
                onTap: () {
                  Navigator.pop(context);
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => const Loginscreen(),
                    ),
                  );
                },
              ),

            ],
          ),
        ),
        appBar: AppBar(
          backgroundColor: Colors.blueAccent,
          title: const Text('Update Profile'),
        ),


        body: Padding(
          padding: const EdgeInsets.all(12.0),
          child: Form(child:Column(
            children: [
              form(name:"Name"),
              const SizedBox(height: 20,),

              form(name:"UserName"),
              const SizedBox(height: 20,),

              form(name:"Email"),
              const SizedBox(height: 20,),

              form(name:"Password"),
              const SizedBox(height: 20,),

              ElevatedButton(onPressed: (){},
                  child: Text('Update Profile')
              ),

            ],

          )),
        )

    );
  }
}