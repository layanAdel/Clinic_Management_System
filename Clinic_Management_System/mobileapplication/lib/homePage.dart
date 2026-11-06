import 'package:flutter/material.dart';
import 'package:mobileapplication/patientsScreen.dart';
import 'package:mobileapplication/prescriptionsScreen.dart';
import 'package:mobileapplication/profile.dart';

import 'appointmentsScreen.dart';
import 'doctorScreen.dart';
import 'loginscreen.dart';


class Homepage extends StatelessWidget {
  const Homepage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: Drawer(
        child: Column(
          children: [
            const UserAccountsDrawerHeader(
              accountName: Text('Layan'),
              accountEmail: Text('L@gmail.com'),
              currentAccountPicture: CircleAvatar(
                backgroundColor: Colors.grey,
                child: Text('LA',
                    style: TextStyle(fontSize: 30,
                        color: Colors.white
                    )),
              ),
              decoration: BoxDecoration(
                color: Colors.blue,
              ),
            ),

            ListTile(
              leading: const Icon(Icons.home),
              title: const Text('Home'),
              onTap:(){
                Navigator.pop(context);
              },
            ),

          //Doctor
            ListTile(
              leading: const Icon(Icons.person),
              title: const Text('Doctor'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const DoctorsScreen(),
                  ),
                );
              },
            ),

            // Patients
            ListTile(
              leading: const Icon(Icons.person),
              title: const Text('Patients'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const PatientsScreen(),
                  ),
                );
              },
            ),

            // Appointments
            ListTile(
              leading: const Icon(Icons.calendar_month),
              title: const Text('Appointments'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const AppointmentsScreen(),
                  ),
                );
              },
            ),

            //Prescriptions
            ListTile(
              leading: const Icon(Icons.medication),
              title: const Text('Prescriptions'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const PrescriptionsScreen(),
                  ),
                );
              },
            ),

            ListTile(
              leading: const Icon(Icons.person),
              title: const Text('Update Profile'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => const Profile(),
                  ),
                );
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
        title: const Text('Home'),
      ),
      body: const Center(
        child: Text('Welcome',
        style: TextStyle(fontSize: 50,
            color: Colors.indigoAccent),
      ),
      ),
    );
  }
}
