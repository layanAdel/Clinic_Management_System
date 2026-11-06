import 'package:flutter/material.dart';

import 'data/conniction.dart';

class PrescriptionDetails extends StatelessWidget {

  final dynamic prescription;

  const PrescriptionDetails(this.prescription, {super.key});

  @override
  Widget build(BuildContext context) {

    // Image Path
    final String imagePath =
        prescription['prescriptionImage']?.toString() ?? '';

    // Complete Image URL
    final String imageUrl = imagePath.isEmpty
        ? ''
        : Uri.parse(ConectionString.dashboardUrl)
        .resolve(imagePath)
        .toString();

    return Scaffold(

      appBar: AppBar(
        title: const Text('Prescription Details'),
        backgroundColor: Colors.indigo,
      ),

      body: SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,

          children: [

            // Prescription Image
            SizedBox(
              width: double.infinity,
              height: 300,

              child: imageUrl.isNotEmpty
                  ? Image.network(
                imageUrl,
                fit: BoxFit.contain,

                errorBuilder: (context, error, stackTrace) {
                  return const Icon(
                    Icons.image_not_supported,
                    size: 100,
                  );
                },
              )
                  : const Icon(
                Icons.image,
                size: 100,
              ),
            ),

            const SizedBox(height: 20),

            Padding(
              padding: const EdgeInsets.all(16),

              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,

                children: [

                  // Medication
                  Text(
                    prescription['medication']?.toString() ?? '',
                    style: const TextStyle(
                      fontSize: 26,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Dosage
                  Text(
                    prescription['dosage']?.toString() ?? '',
                    style: const TextStyle(
                      fontSize: 24,
                      color: Colors.indigo,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 20),

                  const Divider(),

                  const SizedBox(height: 15),

                  // Patient Name
                  Text(
                    'Patient Name: ${prescription['patientName'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Doctor Name
                  Text(
                    'Doctor Name: ${prescription['doctorName'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Instructions
                  Text(
                    'Instructions: ${prescription['instructions'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Prescription Date
                  Text(
                    'Prescription Date: ${prescription['prescriptionDate'] ?? 'N/A'}',
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