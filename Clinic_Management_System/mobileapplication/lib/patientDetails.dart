import 'package:flutter/material.dart';

class PatientDetails extends StatelessWidget {

  final dynamic patient;

  const PatientDetails(this.patient, {super.key});

  @override
  Widget build(BuildContext context) {

    return Scaffold(

      appBar: AppBar(
        title: const Text('Patient Details'),
        backgroundColor: Colors.indigo,
      ),

      body: SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,

          children: [

            // Patient Icon
            SizedBox(
              width: double.infinity,
              height: 300,

              child: const Icon(
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

                  // Patient First Name
                  Text(
                    patient['firstName']?.toString() ?? '',
                    style: const TextStyle(
                      fontSize: 26,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Patient Last Name
                  Text(
                    patient['lastName']?.toString() ?? '',
                    style: const TextStyle(
                      fontSize: 24,
                      color: Colors.green,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 20),

                  const Divider(),

                  const SizedBox(height: 15),

                  // Birth Date
                  Text(
                    'Birth Date: ${patient['birthDate'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Gender
                  Text(
                    'Gender: ${patient['gender'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Phone
                  Text(
                    'Phone: ${patient['phone'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Email
                  Text(
                    'Email: ${patient['email'] ?? 'N/A'}',
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