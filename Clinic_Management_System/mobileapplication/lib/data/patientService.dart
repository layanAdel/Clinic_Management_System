import 'dart:convert';

import 'package:http/http.dart' as http;

import 'conniction.dart';

class PatientService {

  Future<List<dynamic>> getPatients() async {

    final response = await http.get(
      Uri.parse(ConectionString.patients),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load Patients');
    }
  }
}