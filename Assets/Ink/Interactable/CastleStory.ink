Stop right there! Identify yourselves! #GateKeeper
-> Approach_Gate

=== Approach_Gate ===
+   [Netrixi] -> Netrixi_talks_to_GK
+   [Folkvar] -> Folkvar_talks_to_GK
+   [Iv] -> Iv_talks_to_GK

=== Netrixi_talks_to_GK ===
Not a step closer! I can't see a darned thing out there. #GateKeeper
+   [I am Netrixi! The witch you're hunting!] -> Netrixi_reveals_herself
+   [Let us in, or I'll turn you into a cat] -> Netrixi_scares_GK

=== Netrixi_reveals_herself ===
GUARDS! The Witch of the Forest is attempting to breach the Castle! ATTACK! #GateKeeper 
-> Fight_GK

=== Netrixi_scares_GK ===
I sure wish I was a cat... In that case, Guards! attack! #GateKeeper
-> Fight_GK

=== Fight_GK ===
+   [Fight] -> END

=== Folkvar_talks_to_GK ===
Not a step closer! I can't see a darned thing out there. #GateKeeper
+   [It is I, Folkvar!] -> Folkvar_reveals_himself
+   [*Continue walking*] -> Folkvar_scares_GK

=== Folkvar_reveals_himself ===
Folkvar? We were afraid you would never come back from your mission! Hurry inside. #GateKeeper
+   [Continue] -> END

=== Folkvar_scares_GK ===
Intruder alert! It's a bunch of crazy Beholderite addicts storming the castle. We need reinforcements! #GateKeeper
-> Fight_GK

=== Iv_talks_to_GK ===
Not a step closer! I can't see a darned thing out there. #GateKeeper
+   [I am a monk!] -> Iv_reveals_herself
+   [Folkvar is here!] -> Iv_scares_GK

=== Iv_reveals_herself ===
Oh well then why didn't you just say so? Step right up and we will take you inside. #GateKeeper
+   [Continue] -> END

=== Iv_scares_GK ===
The intruders have Folkvar! Guards! Rescue him! #GateKeeper
-> Fight_GK