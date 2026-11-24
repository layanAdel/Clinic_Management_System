import 'package:flutter/material.dart';
import 'package:provider/provider.dart';


import '../../App2/data/appointmentService.dart';
import '../provider/AppointmentProvider.dart';
import '../provider/UserProvider.dart';
import 'appointmentDetails.dart';

class AppointmentsScreen extends StatefulWidget {

  const AppointmentsScreen({super.key});


  @override
  State<AppointmentsScreen> createState() => _AppointmentsScreenState();
}

class _AppointmentsScreenState extends State<AppointmentsScreen> {


  @override
  void initState() {
    super.initState();

    Future.microtask(() {
      if (!mounted) return;
      context.read<AppointmentProvider>().getAppointments();
    });
  }

  @override
  Widget build(BuildContext context) {
    final userProvider = context.watch<Userprovider>();

    return Scaffold(

      appBar: AppBar(
        title: const Text('Appointments'),
        backgroundColor: Colors.indigo,
      ),

      body: Consumer<AppointmentProvider>(
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

          final appointmentsList =  provider.appointments
              .where((appointment) =>
                    appointment['patientId'] == userProvider.patientId)
              .toList();

          if (appointmentsList.isEmpty) {
            return const Center(
              child: Text('No Appointments Found'),
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

            itemCount: appointmentsList.length,

            itemBuilder: (context, index) {

              final appointment = appointmentsList[index];

              return InkWell(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) =>
                          AppointmentDetails(appointment),
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
                            Icons.calendar_month,
                            size: 60,
                            color: Colors.indigo,
                          ),
                        ),
                      ),

                      const SizedBox(height: 10),

                      // Doctor Name
                      Text(
                        appointment['doctorName']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 15,
                          color: Colors.indigo,
                          fontWeight: FontWeight.bold,
                        ),
                      ),

                      const SizedBox(height: 10),

                      // Appointment Date
                      Text(
                        appointment['appointmentDate']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 14,
                        ),
                        maxLines: 1,
                        overflow: TextOverflow.ellipsis,
                      ),

                      const SizedBox(height: 10),

                      // Status
                      Text(
                        appointment['status']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 15,
                          color: Colors.indigo,
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