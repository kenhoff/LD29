2014/04/25 10:00am
------------------

> Here's the post that I published on the Ludum Dare blog!

I'm in. My main goal for this compo is to strictly only use C# with Unity. I've been a Unity developer for years, but I've only used Javascript and I've begun to run into technical limitations.

Tool list:

 - Engine: Unity
 - Editor: Sublime, most likely with Vintageous
 - Art: Procreate for iPad
 - Sound: Bfxr, audacity, whatever samples aren't too outrageous
 - Timelapse: Chronolapse
 - Version control: Git, Github
 - Sustenance: Tomato soup, crusty bread, chai, lots of fruit
 - Working music: Beatbuddy Soundtrack on endless repeat

That's all I can think of for now. Best of luck to all entrants!

2014/04/25 7:47pm
-----------------

> Beneath the Surface

The theme got announced while I was in the middle of a Binding of Isaac game, so I finished that while letting the theme run on a mental background process. Lost on the final stage of Satan (arrrrgh)

First things that came to mind were 2D-side scroller sea game. Just like Aquaria, Beatbuddy, those sort of games. I remember a while back that there was a simple flash game titled "Moby Dick." The premise behind it was that someone had written a game about Moby Dick without reading the book and having a very limited idea of what it was about. It consisted of a white whale swimming around the sea absolutely wrecking every bit of sailor and ship it could find. Totally silly, tons of fun.

A track came to mind as soon as I heard the theme, too. "Orange's Kiss," off the Castle Crashers soundtrack, has that super-cheesy maritime theme in it, and I'm definetely using it as placeholder music for the time being.

I'm thinking a spiritual successor to that Moby Dick game, a la Luftrausers. Your submarine turns towards the mouse cursor, right click to go, left click to shoot. Collect upgrades for your health, weapons and engines, and fight other submarines, destroyers, carriers, getting shot at with torpedoes, depth charges, harpoons, kamikaze seagulls, that sort of thing.

I don't want it to be a direct rip, though. There's gotta be something else that I can include to really make it different. Hmmm....

Next steps are figuring out some big design decisions, then creating a backlog, iterating a few times, then getting to building.

Exciting!

2014/04/25 9:23pm
-----------------

Okay. Nachos created, nachos eaten. I've had some more time to think about this.

I definitely want to put an old-timey spin on this, sort of a whaling-era thing. The submersible will be reminicient of a bathysphere, and the goal right now is to just fight off sharks; something about eating all the fish, or bounties on sharks, that sort of thing. Your captain/mentor/advisor person will be kind of like Popeye, with a beard, gone slightly crazy. 

You'll use your torpedoes to destroy everything; sharks, cannonballs, depth charges, other ships, prehistoric leviathans. 

One big question I'll have to deal with is progression. Is this a one-time playthrough? Do you only get one life, and try to rack up as many points as possible, collecting powerups along the way? Or should I go the Luftrausers/Burrito Bison route, and have you power up every time you die? I do like that one, but to be honest that's a lot of menu crafting that I'm not sure I'm ready for. 

One cool thing about that is that I can have big, overarching bosses that serve as checkpoints along the way. If I get everything else working, those might be fun to code.

As for powerups, will increases in health, speed, and damage be enough? I'm worried that it won't be. Perhaps if you upgrade your weapon, the speed and explosion radius will increase, as well as the damage. 

That might be something I can work out a little later. If I get the basic gameplay working, with the appropriate ramp-up of standard enemies, then I can start working in an upgrade system.

Backlog time!

2014/04/26 12:19am
------------------

Just had a pretty major crash. Hopefully I haven't lost too much progress.

Looks like it was mostly just the prefabs in the editor. Sucks, but it'll be pretty straightforward to redo. Lesson learned. Signing off til tomorrow.

2014/04/26 11:36am
------------------

Hokays, looks like I just have to redo all of the prefabs. There weren't too many, so it'll be easy.

I was having trouble with the movement that I was looking for, so I checked out one of my old projects and was able to find the code. It's basically having the player rotate towards a given angle with a dampened torque, rather than just having the transform point in the right direction. It looks pretty cool!

I'm working on the sea particles now, and trying not to make it look like space. Maybe just a small rotation or movement will be a good effect for that.

2014/04/26 2:26pm
-----------------

Had another crash >:[ I don't think I lost anything this time though. I was slowing down a bit, so I took a break and finally beat FTL for the first time. Woohoo!

Working on sharks now. There's some spawning issues, so I have to take care of those before I can really get to work on the AI.

2014/04/27 9:56am
-----------------

Artwork is in, basic gameplay is working. I think that the next goal for me is to add sounds, music, and particle effects for thrusting. And art for explosions and bubbles.

As for expanding gameplay, I think for now I'm just going to start spawning larger and larger sharks. Another thing I have to think about is world boundaries; should I keep rock walls and floors, and somehow figure out the surface constraint? Or should I figure out the endless looping, like in Luftrausers? It'd be an awesome effect, and if I could do it reliably it'd be much simpler than world constraints.

Before all that, though, I have to adjust the torpedo explosion force and damage. Excelsior!

2014/04/27 3:22pm
-----------------

Welp, I'm calling it. It's been a ton of fun, but I'm pretty burnt out on it right now. All the basic gameplay is in there. Things I would like to include eventually:

 - Sound
 - Giblets
 - Increasing sizes and difficulties of sharks
 - An upgrade screen, where you can spend your hard-earned shark oil dubloons on torpedo and ship upgrades

 It's been fun! Time to write the final post, package it and put it online, along with source and timelapse stuff.