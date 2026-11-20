import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'App2/provider/CartProvider.dart';
import 'App2/provider/DoctorProvider.dart';
import 'App2/provider/OrderProvider.dart';
import 'App2/provider/PatientProvider.dart';
import 'App2/provider/PrescriptionProvider.dart';
import 'App2/provider/UserProvider.dart';
import 'App2/screens/loginscreen.dart';
import 'App2/provider/AppointmentProvider.dart';
import 'App2/screens/patientsScreen.dart';


//void main() {
  //runApp(const MyApp()); }


void main() {
  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(
          create: (context) => Userprovider(),
        ),

        ChangeNotifierProvider(
          create: (context) => AppointmentProvider(),
        ),


        ChangeNotifierProvider(
          create: (context) => DoctorProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => PrescriptionProvider(),
        ),

        ChangeNotifierProvider(
          create: (context) => CartProvider(),
        ),

        ChangeNotifierProvider(
            create: (context) => OrderProvider()),

        ChangeNotifierProvider(
            create: (context) => PatientProvider()),

      ],


     child: const MyApp(),

    )
  );
}





class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        colorScheme: .fromSeed(seedColor: Colors.deepPurple),
      ),
      home: PatientsScreen(),
      debugShowCheckedModeBanner: false,
    );
  }
}

