import 'package:flutter/material.dart';
import '../data/appointmentService.dart';

class AppointmentProvider extends ChangeNotifier {

  final AppointmentService appointmentService = AppointmentService();

  List<dynamic> appointments = [];

  bool isLoading = false;

  String? error;

  Future<void> getAppointments() async {

    isLoading = true;
    error = null;
    notifyListeners();

    try {
      appointments = await appointmentService.getAppointments();
    }
    catch (e) {
      error = 'Failed to load appointments';
    }
    finally {
      isLoading = false;
      notifyListeners();
    }
  }
}