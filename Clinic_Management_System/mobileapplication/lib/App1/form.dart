import 'package:flutter/material.dart';

class form extends StatelessWidget {
   form({super.key, required this.name});

  String name;
  @override
  Widget build(BuildContext context) {
    return TextFormField(
      decoration: InputDecoration(
        labelText: "$name",
        enabledBorder: OutlineInputBorder(
          borderSide: BorderSide(
            color: Colors.blueGrey,
            width: 2,
          ),
          borderRadius: BorderRadius.all(Radius.circular(40))
        )
      ),
      validator: (value){
        if(value == null || value.isEmpty){
          return "Please Enter your $name";
        }
        return null;
      },

    );
  }
}
