import 'package:flutter/material.dart';
import '../data/PatientService.dart';

class PatientProvider extends ChangeNotifier {

  final PatientService patientService = PatientService();

  List<dynamic> patients = [];

  bool isLoading = false;
  String? error;

  Future<void> getPatients() async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      patients = await patientService.getPatients();
    } catch (e) {
      error = 'Failed to load patients';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}