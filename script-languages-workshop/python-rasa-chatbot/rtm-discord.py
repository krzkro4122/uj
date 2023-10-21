import discord

class MyClient(discord.Client):
    async def on_ready(self):
        print('Logged on as', self.user)

    async def on_message(self, message):
        # don't respond to ourselves
        if message.author == self.user:
            return

        if 'Hello' in message.content:
            await message.channel.send('World!')

intents = discord.Intents.default()
intents.message_content = True
client = MyClient(intents=intents)
client.run('MTA0Njg1NjI3Nzk2MTIyNDI0Mg.GMGmPO.hWk-_yUygSTIBamvDnJGWb9fa9jIww14gkegkg')