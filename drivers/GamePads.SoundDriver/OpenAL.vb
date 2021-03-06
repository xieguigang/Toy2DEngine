﻿'Imports System.Diagnostics
'Imports System.Threading
'Imports System.IO

'Imports OpenTK.Audio
'Imports OpenTK.Audio.OpenAL

'''' <summary>
'''' OpenAl Playback device
'''' </summary>
'Public Module OpenALPlayback

'    ''' <summary>
'    ''' Loads a wave/riff audio file.
'    ''' </summary>
'    ''' <param name="stream"></param>
'    ''' <param name="channels"></param>
'    ''' <param name="bits"></param>
'    ''' <param name="rate"></param>
'    ''' <returns></returns>
'    Public Function LoadWave(stream As Stream, ByRef channels As Integer, ByRef bits As Integer, ByRef rate As Integer) As Byte()
'        If stream Is Nothing Then
'            Throw New ArgumentNullException("stream")
'        End If

'        Using reader As New BinaryReader(stream)
'            ' RIFF header
'            Dim signature As New String(reader.ReadChars(4))
'            If signature <> "RIFF" Then
'                Throw New NotSupportedException("Specified stream is not a wave file.")
'            End If

'            Dim riff_chunck_size As Integer = reader.ReadInt32()

'            Dim format As New String(reader.ReadChars(4))
'            If format <> "WAVE" Then
'                Throw New NotSupportedException("Specified stream is not a wave file.")
'            End If

'            ' WAVE header
'            Dim format_signature As New String(reader.ReadChars(4))
'            If format_signature <> "fmt " Then
'                Throw New NotSupportedException("Specified wave file is not supported.")
'            End If

'            Dim format_chunk_size As Integer = reader.ReadInt32()
'            Dim audio_format As Integer = reader.ReadInt16()
'            Dim num_channels As Integer = reader.ReadInt16()
'            Dim sample_rate As Integer = reader.ReadInt32()
'            Dim byte_rate As Integer = reader.ReadInt32()
'            Dim block_align As Integer = reader.ReadInt16()
'            Dim bits_per_sample As Integer = reader.ReadInt16()

'            Dim data_signature As New String(reader.ReadChars(4))
'            If data_signature <> "data" Then
'                Throw New NotSupportedException("Specified wave file is not supported.")
'            End If

'            Dim data_chunk_size As Integer = reader.ReadInt32()

'            channels = num_channels
'            bits = bits_per_sample
'            rate = sample_rate

'            Return reader.ReadBytes(CInt(reader.BaseStream.Length))
'        End Using
'    End Function

'    Public Function GetSoundFormat(channels As Integer, bits As Integer) As ALFormat
'        Select Case channels
'            Case 1
'                Return If(bits = 8, ALFormat.Mono8, ALFormat.Mono16)
'            Case 2
'                Return If(bits = 8, ALFormat.Stereo8, ALFormat.Stereo16)
'            Case Else
'                Throw New NotSupportedException("The specified sound format is not supported.")
'        End Select
'    End Function

'    ''' <summary>
'    '''
'    ''' </summary>
'    ''' <param name="filename">Only supports wav???</param>
'    Public Sub PlaySound(filename As String)
'        Using context As New AudioContext()
'            Dim buffer As Integer = AL.GenBuffer()
'            Dim source As Integer = AL.GenSource()
'            Dim state As Integer
'            Dim channels As Integer, bitsPerSample As Integer, sampleRate As Integer
'            Dim soundBuf As Byte() = LoadWave(File.Open(filename, FileMode.Open), channels, bitsPerSample, sampleRate)

'            Call AL.BufferData(buffer, GetSoundFormat(channels, bitsPerSample), soundBuf, soundBuf.Length, sampleRate)
'            Call AL.Source(source, ALSourcei.Buffer, buffer)
'            Call AL.SourcePlay(source)

'            ' Query the source to find out when it stops playing.
'            Do
'                Call Thread.Sleep(250)
'                Call AL.GetSource(source, ALGetSourcei.SourceState, state)
'            Loop While DirectCast(state, ALSourceState) = ALSourceState.Playing

'            Call AL.SourceStop(source)
'            Call AL.DeleteSource(source)
'            Call AL.DeleteBuffer(buffer)
'        End Using
'    End Sub
'End Module
