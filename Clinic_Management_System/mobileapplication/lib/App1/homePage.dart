import 'package:flutter/material.dart';

import '../App1/prescriptionsScreen.dart';
import '../App1/profile.dart';
import 'appointmentsScreen.dart';
import 'cartScreen.dart';
import 'doctorScreen.dart';
import 'loginscreen.dart';
import 'myOrderScreen.dart';


class Homepage extends StatelessWidget {
  final int patientId;

  const Homepage(this.patientId, {super.key});


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

            //Update Profile
            ListTile(
              leading: const Icon(Icons.person),
              title: const Text('Update Profile'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => Profile(patientId),
                  ),
                );
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


            // Appointments
            ListTile(
              leading: const Icon(Icons.calendar_month),
              title: const Text('Appointments'),
              onTap: () {
                Navigator.pop(context);
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => AppointmentsScreen(patientId),
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


            // Cart
            ListTile(
              leading: const Icon(Icons.shopping_cart),
              title: const Text('Cart'),
              onTap: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) =>
                        CartScreen(patientId),
                  ),
                );
              },
            ),

            //Order
            ListTile(
              leading: const Icon(Icons.shopping_bag),
              title: const Text('My Bookings'),
              onTap: () {
                Navigator.pop(context);

                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => MyOrdersScreen(patientId),
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
