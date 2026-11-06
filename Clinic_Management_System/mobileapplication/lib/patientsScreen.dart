import 'package:flutter/material.dart';
import 'package:mobileapplication/patientDetails.dart';

import 'data/PatientService.dart';


class PatientsScreen extends StatefulWidget {
  const PatientsScreen({super.key});

  @override
  State<PatientsScreen> createState() => _PatientsScreenState();
}

class _PatientsScreenState extends State<PatientsScreen> {

  final PatientService patientService = PatientService();

  late Future<List<dynamic>> patients;

  @override
  void initState() {
    super.initState();
    patients = patientService.getPatients();
  }

  @override
  Widget build(BuildContext context) {

    return Scaffold(

      appBar: AppBar(
        title: const Text('Patients'),
        backgroundColor: Colors.indigo,
      ),

      body: FutureBuilder<List<dynamic>>(
        future: patients,

        builder: (context, snapshot) {

          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }

          if (snapshot.hasError) {
            return Center(
              child: Text('Error: ${snapshot.error}'),
            );
          }

          final patientsList = snapshot.data ?? [];

          if (patientsList.isEmpty) {
            return const Center(
              child: Text('No Patients Found'),
            );
          }

          return GridView.builder(

            padding: const EdgeInsets.all(12),

            gridDelegate:
            const SliverGridDelegateWithFixedCrossAxisCount(
              crossAxisCount: 2,
              crossAxisSpacing: 10,
              mainAxisSpacing: 10,
              childAspectRatio: 0.75,
            ),

            itemCount: patientsList.length,

            itemBuilder: (context, index) {

              final patient = patientsList[index];

              return InkWell(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) =>
                          PatientDetails(patient),
                    ),
                  );
                },

                child: Card(
                  elevation: 4,

                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(15),
                  ),

                  child: Column(
                    children: [

                      Expanded(
                        child: Center(
                          child: Icon(
                            Icons.person,
                            size: 60,
                            color: Colors.indigo,
                          ),
                        ),
                      ),

                      const SizedBox(height: 10),

                      // Patient First Name
                      Text(
                        patient['firstName']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.bold,
                        ),
                        maxLines: 1,
                        overflow: TextOverflow.ellipsis,
                      ),

                      const SizedBox(height: 5),

                      // Patient Last Name
                      Text(
                        patient['lastName']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 15,
                          color: Colors.indigo,
                          fontWeight: FontWeight.bold,
                        ),
                      ),

                      const SizedBox(height: 10),

                      // Gender
                      Text(
                        patient['gender']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 15,
                        ),
                      ),

                      const SizedBox(height: 10),

                      // Phone
                      Text(
                        patient['phone']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 15,
                          color: Colors.green,
                          fontWeight: FontWeight.bold,
                        ),
                      ),

                      const SizedBox(height: 10),
                    ],
                  ),
                ),
              );
            },
          );
        },
      ),
    );
  }
}