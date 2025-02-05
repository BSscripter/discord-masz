import os

import discord
from discord.errors import LoginFailure

from helpers import console
from commands import on_command_error as _on_command_error
from automod import check_message
from punishment import handle_member_join as handle_punishment_on_member_join
from client import client, slash


@client.event
async def on_command_error(ctx, error):
    await _on_command_error(ctx, error)

@client.event
async def on_slash_command_error(ctx, error):
    await _on_command_error(ctx, error)

@client.event
async def on_member_join(member):
    console.info(f"{member} joined the server '{member.guild}'.")
    await handle_punishment_on_member_join(member)

@client.event
async def on_ready():
    console.info(f"Logged in as \"{client.user.name}\"")
    console.info(f"Online in {len(client.guilds)} Guilds.")

    await slash.sync_all_commands()

    activity = os.getenv("META_SERVICE_BASE_URL", "github.com/zaanposni/discord-masz")
    if activity:
        game = discord.Game(name=activity)
        await client.change_presence(activity=game)
        console.info(f"Set status: \"{game.name}\".")

@client.event
async def on_guild_join(guild):
    console.info(f"Joined guild \"{guild}\".")
    await slash.sync_all_commands()

@client.event
async def on_message(msg):
    moderated = await check_message(msg)
    if not moderated:
        await client.process_commands(msg)


@client.event
async def on_raw_message_edit(payload):
    channel = client.get_channel(payload.channel_id)
    if not channel:
        return
    try:
        msg = await channel.fetch_message(payload.message_id)
    except Exception as e:  # not found, forbidden, similar
        console.critical(f"Failed to fetch message on edit - {e}")
        return
    
    await check_message(msg, True)

def start_bot():
    try:
        token = os.getenv("DISCORD_BOT_TOKEN")
    except KeyError:
        console.critical(f"========================")
        console.critical(f"'DISCORD_BOT_TOKEN' not found in env!")
        return
    try:
        console.info(f"Logging in.")
        client.run(token, reconnect=True)
    except LoginFailure:
        console.critical(f"========================")
        console.critical(f"Login failure!")
        console.critical(f"Please check your token.")
        return


start_bot()
