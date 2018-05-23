﻿
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using System.Threading;
using Windows.Foundation;
using AudioVisualizer;
using System.Collections.Generic;

namespace test.managed
{
    [TestClass]
    public class PlaybackSourceTests
    {

        [TestMethod]
        [TestCategory("PlaybackSource")]
        public async Task PlaybackSource_MediaPlayer()
        {
            var testFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///TestContent/test_signal.mp3"));
            var testSourceFromFile = MediaSource.CreateFromStorageFile(testFile);

            var player = new MediaPlayer();
            var playbackSource = new AudioVisualizer.PlaybackSource(player);
            List<IVisualizationSource> sources = new List<IVisualizationSource>();
            ManualResetEventSlim ev = new ManualResetEventSlim();

            playbackSource.SourceChanged += new TypedEventHandler<object, IVisualizationSource>(
                (sender,source)=>
                {
                    sources.Add(source);
                    ev.Set();
                }
                );

            player.Source = testSourceFromFile;
            player.Play();

            if (Task.Run(() => { ev.Wait(); }).Wait(1000))

            {
                Assert.IsTrue(sources.Count == 1);
                Assert.IsNotNull(sources[0]);
                Assert.IsInstanceOfType(sources[0], typeof(AudioVisualizer.MediaAnalyzer));
            }
            else
                Assert.Fail("Timeout when waiting for the source creation");
        }

    }
}
