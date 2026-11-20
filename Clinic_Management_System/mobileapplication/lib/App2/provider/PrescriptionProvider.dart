import 'package:flutter/material.dart';
import '../data/prescriptionService.dart';

class PrescriptionProvider extends ChangeNotifier {

  final PrescriptionService prescriptionService = PrescriptionService();

  List<dynamic> prescriptions = [];

  bool isLoading = false;

  String? error;

  Future<void> getPrescriptions() async {

    isLoading = true;
    error = null;
    notifyListeners();

    try {
      prescriptions = await prescriptionService.getPrescriptions();
    }
    catch (e) {
      error = 'Failed to load prescriptions';
    }
    finally {
      isLoading = false;
      notifyListeners();
    }
  }
}