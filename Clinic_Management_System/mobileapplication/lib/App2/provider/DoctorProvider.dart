import 'package:flutter/material.dart';
import '../data/doctorService.dart';

class DoctorProvider extends ChangeNotifier {

  final DoctorService doctorService = DoctorService();

  List<dynamic> doctors = [];

  bool isLoading = false;

  String? error;

  Future<void> getDoctors() async {

    isLoading = true;
    error = null;
    notifyListeners();

    try {
      doctors = await doctorService.getDoctors();
    }
    catch (e) {
      error = 'Failed to load doctors';
    }
    finally {
      isLoading = false;
      notifyListeners();
    }
  }
}