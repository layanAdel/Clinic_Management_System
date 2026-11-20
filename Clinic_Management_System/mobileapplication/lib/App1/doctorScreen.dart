import 'package:flutter/material.dart';

import 'data/DoctorService.dart';
import 'doctorDetails.dart';

class DoctorsScreen extends StatefulWidget {
  const DoctorsScreen({super.key});

  @override
  State<DoctorsScreen> createState() => _DoctorsScreenState();
}

class _DoctorsScreenState extends State<DoctorsScreen> {

  final DoctorService doctorService = DoctorService();

  late Future<List<dynamic>> doctors;

  @override
  void initState() {
    super.initState();
    doctors = doctorService.getDoctors();
  }

  @override
  Widget build(BuildContext context) {

    return Scaffold(

      appBar: AppBar(
        title: const Text('Doctors'),
        backgroundColor: Colors.indigo,
      ),

      body: FutureBuilder<List<dynamic>>(
        future: doctors,

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

          final doctorsList = snapshot.data ?? [];

          if (doctorsList.isEmpty) {
            return const Center(
              child: Text('No Doctors Found'),
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

            itemCount: doctorsList.length,

            itemBuilder: (context, index) {

              final doctor = doctorsList[index];

              return InkWell(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) =>
                          DoctorDetails(doctor),
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

                      const Expanded(
                        child: Center(
                          child: Icon(
                            Icons.person,
                            size: 60,
                          ),
                        ),
                      ),

                      const SizedBox(height: 10),

                      Text(
                        '${doctor['firstName']} ${doctor['lastName']}',
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.bold,
                        ),
                        maxLines: 1,
                        overflow: TextOverflow.ellipsis,
                      ),

                      const SizedBox(height: 5),

                      Text(
                        doctor['specialization']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 15,
                          color: Colors.green,
                          fontWeight: FontWeight.bold,
                        ),
                      ),

                      const SizedBox(height: 10),

                      Text(
                        doctor['departmentName']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 15,
                          color: Colors.green,
                          fontWeight: FontWeight.bold,
                        ),
                      ),

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