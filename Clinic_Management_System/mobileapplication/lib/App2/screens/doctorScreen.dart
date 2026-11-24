import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../provider/DoctorProvider.dart';
import 'doctorDetails.dart';

class DoctorsScreen extends StatefulWidget {
  const DoctorsScreen({super.key});

  @override
  State<DoctorsScreen> createState() => _DoctorsScreenState();
}

class _DoctorsScreenState extends State<DoctorsScreen> {
  @override
  void initState() {
    super.initState();

    Future.microtask(() {
      if (!mounted) return;
      context.read<DoctorProvider>().getDoctors();
    });
  }

  @override
  Widget build(BuildContext context) {

    return Scaffold(

      appBar: AppBar(
        title: const Text('Doctors'),
        backgroundColor: Colors.indigo,
      ),

      body: Consumer<DoctorProvider>(
        builder: (context, provider, child) {

          if (provider.isLoading) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }

          if (provider.error != null) {
            return Center(
              child: Text(provider.error!),
            );
          }

          final doctorsList = provider.doctors;

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