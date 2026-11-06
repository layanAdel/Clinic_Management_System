import 'package:flutter/material.dart';

import 'data/userservice.dart';
import 'homePage.dart';
import 'loginscreen.dart';

class Signup extends StatefulWidget {
  const Signup({super.key});

  @override
  State<Signup> createState() => _SignupState();
}

class _SignupState extends State<Signup> {
  TextEditingController namecontroller = TextEditingController();

  TextEditingController emailcontroller = TextEditingController();

  TextEditingController usernamecontroller = TextEditingController();

  TextEditingController passwordcontroller = TextEditingController();

  UserService userService = UserService();
  bool isLoading = false;


  void signup(BuildContext context) async {
    String name = namecontroller.text.trim();
    String email = emailcontroller.text.trim();
    String username = usernamecontroller.text.trim();
    String password = passwordcontroller.text;

    if (name.isEmpty || email.isEmpty ||
        username.isEmpty || password.isEmpty ) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content:
        Text('Please enter all fildes')),
      );
      return;
    }
    setState(() {
      isLoading = true;
    });
    try {
      final user  = await userService.register(
          name,
          email,
          username,
          password
      );

      if (!context.mounted) return;
      if (user != null) {
        Navigator.pushReplacement(
          context,
          MaterialPageRoute(builder: (context) => Homepage()),
        );
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: const Text('Username or email already exists'),
            behavior: SnackBarBehavior.floating,
            duration: const Duration(seconds: 3),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(20),
            ),
            backgroundColor: Colors.black38,
          ),
        );
      }
    } catch (e) {
      if (!context.mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Failed to connect to server'),
          backgroundColor: Colors.red,
        ),
      );
    } finally {
      if (mounted) {
        setState(() {
          isLoading = false;
        });
      }
    }
  }


  @override
  void dispose() {
    namecontroller.dispose();
    emailcontroller.dispose();
    usernamecontroller.dispose();
    passwordcontroller.dispose();
    super.dispose();
  }



  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Sign Up'),
        backgroundColor: Colors.indigo,
      ),

      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16.0),

          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,

            children: [
              const SizedBox(height: 50),

              const Icon(Icons.person, size: 100, color: Colors.indigo),

              TextField(
                controller: namecontroller,

                decoration: const InputDecoration(
                  labelText: 'Name',
                  border: OutlineInputBorder(),
                  prefixIcon: Icon(Icons.person),
                ),
              ),

              TextField(
                controller: emailcontroller,

                decoration: const InputDecoration(
                  labelText: 'Email',
                  border: OutlineInputBorder(),
                  prefixIcon: Icon(Icons.email),
                ),
              ),

              TextField(
                controller: usernamecontroller,

                decoration: const InputDecoration(
                  labelText: 'Username',
                  border: OutlineInputBorder(),
                  prefixIcon: Icon(Icons.person),
                ),
              ),

              const SizedBox(height: 20),

              TextField(
                controller: passwordcontroller,
                obscureText: true,

                decoration: const InputDecoration(
                  labelText: 'Password',
                  border: OutlineInputBorder(),
                  prefixIcon: Icon(Icons.lock),
                ),
              ),

              const SizedBox(height: 20),

              ElevatedButton(
                onPressed: isLoading ? null : () => signup(context),

                child: isLoading
                    ? const CircularProgressIndicator()
                    : const Text('Sign Up'),
              ),

              const SizedBox(height: 20),

              Row(
                mainAxisAlignment: MainAxisAlignment.center,

                children: [
                  const Text('Already have an account? '),

                  GestureDetector(
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(builder: (context) => Loginscreen()),
                      );
                    },

                    child: const Text(
                      ' Login',
                      style: TextStyle(
                        decoration: TextDecoration.underline,
                        color: Colors.blue,
                      ),
                    ),
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }
}