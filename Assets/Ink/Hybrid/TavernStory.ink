Ahhh that's the stuff. The only thing that takes me back to my childhood like Beholderite does is a nice cold brew. #Netrixi

How are you so relaxed? We barely got out of there alive! #Folkvar

You learn some things when you're alive for hundreds of years. I won't be able to save us next time though. #Netrixi

Hmmm. Ok well we might need some extra help, and not just with the lad that stole our compass. #Folkvar 
Whoever has that Beholderite probably has it guarded. Shall we ask someone in the tavern to assist us? #Folkvar

+   [Brute] -> Brute
+   [Monk] -> Resort_to_monk

=== Brute ===
Good choice, Folkvar. Do you want to do the talking? I usually scare guys like him away. #Netrixi

+   [Netrixi] -> Netrixi_talks_to_Brute
+   [Folkvar] -> Folkvar_talks_to_Brute

=== Netrixi_talks_to_Brute ===
What do you want, little girl? #Brute

+   [I offer beholderite] -> Brute_likes_beholderite
+   [Are those pretty muscles?] -> Fight_Brute

=== Folkvar_talks_to_Brute ===
What do you want, preppy little man? #Brute

+   [We need help finding Beholderite] -> Brute_likes_beholderite
+   [We would like to hire your services] -> Brute_prostitute

=== Brute_likes_beholderite ===
BEHOLDERITE?! I WILL DESTROY ANYONE WHO COMES BETWEEN ME AND BEHOLDERITE!!! WHERE IS IT?! #Brute

+   [You're crazy. Bye] -> Resort_to_monk

=== Brute_prostitute ===
You think I'm some kind of prostitute or something? That's stereotyping! #Brute

+   [Fight] -> END

=== Fight_Brute ===
I'll squash you! #Brute

+   [Fight] -> END

=== Resort_to_monk ===
Yeah, that other guy seemed a little crazy. Let's go talk to the bald one. #Netrixi

+   [Netrixi] -> Netrixi_talks_to_Iv
+   [Folkvar] -> Folkvar_talks_to_Iv

=== Netrixi_talks_to_Iv ===
Please don't waste my time. #Netrixi

Can I help you? #Iv

+   [Are you a fighter?] -> Iv_shows_power
+   [Why are you here?] -> Netrixi_asks_Iv_why_here

=== Iv_shows_power ===
Watch this... #Iv

Wow I feel powerful! #Netrixi

What? What have you done to me? I'm suddenly so weak! #Netrixi

I can do stuff like that. #Iv

WE NEED HER IN THE GANG! #Netrixi

I will remind you that first, I must ask my father for permission to commence the bashing of skulls. #Folkvar

+   [Next Scene] -> END

=== Netrixi_asks_Iv_why_here ===
I am a monk from the mountains to the East looking for my brother. I am looking for help in finding my brother. #Iv

I believe he was taken as a slave to mine Beholderite. #Iv

Oooo that's rough. Wait, I didn't think there were Beholderite mines anymore. I thought they were all outlawed. #Netrixi

A man named Moke is running a secret operation. Still, it is difficult to keep an operation that big a secret. #Iv

I think I might be able to find your brother. The problem is... #Netrixi

+   [How can I trust you?] -> Iv_explains_past
+   [You clearly can't fight] -> Iv_shows_power

=== Iv_explains_past ===
I have trained my entire life to rid myself of worldly temptations. Beholderite simply does not phase me. #Iv

Welcome aboard, then! Sadly, we can't start knocking skulls yet. Folkvar needs to ask daddy first. #Netrixi

+   [Next Scene] -> END

=== Folkvar_talks_to_Iv ===

Well hello there, little lady! I hope you do not mind me saying but you look oddly out of place here! #Folkvar

I am a monk from the mountains to the East. I am looking for help in finding my brother. #Iv

I believe he was taken as a slave to mine Beholderite. #Iv

By God! Excuse my language. Where do you think your brother was taken? By whom? #Folkvar

I hear rumors of a man named Moke. The problem is, I have no idea where Moke's lair may be. #Iv


+   [We can help each other!] -> Folkvar_offers_Iv_help
+   [Can you take care of yourself?] -> Iv_shows_power

=== Folkvar_offers_Iv_help ===
I think I know where your brother is. We are going to the same place. #Folkvar

+   [Are you obsessed with beholderite?] -> Iv_explains_past
+   [Can you take care of yourself?] -> Iv_shows_power