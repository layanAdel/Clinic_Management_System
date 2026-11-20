import 'package:flutter/material.dart';

class DoctorDetails extends StatelessWidget {

  final dynamic doctor;

  const DoctorDetails(this.doctor, {super.key});

  @override
  Widget build(BuildContext context) {

    return Scaffold(

      appBar: AppBar(
        title: const Text('Doctor Details'),
        backgroundColor: Colors.indigo,
      ),

      body: SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,

          children: [

            // Doctor Icon
            const SizedBox(
              width: double.infinity,
              height: 300,

              child: Icon(
                Icons.person,
                size: 100,
              ),
            ),

            const SizedBox(height: 20),

            Padding(
              padding: const EdgeInsets.all(16),

              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,

                children: [

                  // Doctor Name
                  Text(
                    (doctor['firstName']?.toString() ?? '') + ' ' +
                    (doctor['lastName']?.toString() ?? ''),                    style: const TextStyle(
                      fontSize: 26,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Doctor Specialization
                  Text(
                    doctor['specialization']?.toString() ?? '',
                    style: const TextStyle(
                      fontSize: 24,
                      color: Colors.green,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 20),

                  const Divider(),

                  const SizedBox(height: 15),

                  // Doctor Department
                  Text(
                    'Department Name: ${doctor['departmentName'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Doctor Phone
                  Text(
                    'Phone: ${doctor['phone'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Doctor Email
                  Text(
                    'Email: ${doctor['email'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}