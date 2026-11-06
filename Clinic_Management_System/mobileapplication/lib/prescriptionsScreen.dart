
import 'package:flutter/material.dart';
import 'package:mobileapplication/prescriptionDetails.dart';

import 'data/conniction.dart';
import 'data/prescriptionService.dart';

class PrescriptionsScreen extends StatefulWidget {
  const PrescriptionsScreen({super.key});

  @override
  State<PrescriptionsScreen> createState() => _PrescriptionsScreenState();
}

class _PrescriptionsScreenState extends State<PrescriptionsScreen> {

  final PrescriptionService prescriptionService = PrescriptionService();

  late Future<List<dynamic>> prescriptions;

  @override
  void initState() {
    super.initState();
    prescriptions = prescriptionService.getPrescriptions();
  }

  @override
  Widget build(BuildContext context) {

    return Scaffold(

      appBar: AppBar(
        title: const Text('Prescriptions'),
        backgroundColor: Colors.indigo,
      ),

      body: FutureBuilder<List<dynamic>>(
        future: prescriptions,

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

          final prescriptionsList = snapshot.data ?? [];

          if (prescriptionsList.isEmpty) {
            return const Center(
              child: Text('No Prescriptions Found'),
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

            itemCount: prescriptionsList.length,

            itemBuilder: (context, index) {

              final prescription = prescriptionsList[index];

              final String imagePath =
                  prescription['prescriptionImage']?.toString() ?? '';

              final String imageUrl = imagePath.isEmpty
                  ? ''
                  : Uri.parse(ConectionString.dashboardUrl)
                  .resolve(imagePath)
                  .toString();

              return InkWell(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) =>
                          PrescriptionDetails(prescription),
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
                        child: ClipRRect(
                          borderRadius: const BorderRadius.vertical(
                            top: Radius.circular(15),
                          ),

                          child: imageUrl.isNotEmpty
                              ? Image.network(
                            imageUrl,
                            width: double.infinity,
                            fit: BoxFit.cover,

                            errorBuilder: (context, error, stackTrace) {
                              return const Center(
                                child: Icon(
                                  Icons.image_not_supported,
                                  size: 60,
                                ),
                              );
                            },
                          )
                              : const Center(
                            child: Icon(
                              Icons.image,
                              size: 60,
                            ),
                          ),
                        ),
                      ),

                      const SizedBox(height: 10),

                      Text(
                        prescription['medication']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.bold,
                        ),
                        maxLines: 1,
                        overflow: TextOverflow.ellipsis,
                      ),

                      const SizedBox(height: 5),

                      Text(
                        prescription['patientName']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 15,
                          color: Colors.indigo,
                          fontWeight: FontWeight.bold,
                        ),
                      ),

                      const SizedBox(height: 10),

                      Text(
                        prescription['doctorName']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 15,
                          color: Colors.indigo,
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