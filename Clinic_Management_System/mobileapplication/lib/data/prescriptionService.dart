import 'dart:convert';

import 'package:http/http.dart' as http;

import 'conniction.dart';

class PrescriptionService{

  Future<List<dynamic>> getPrescriptions() async {

    final response = await http.get(
      Uri.parse(ConectionString.prescriptions),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load prescriptions');
    }
  }


}